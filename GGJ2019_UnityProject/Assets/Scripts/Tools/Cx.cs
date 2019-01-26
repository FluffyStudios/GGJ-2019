using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FluffyTools
{ 
    public static class Cx
    {
        public abstract class Routine
        {
            public abstract bool isDone { get; }

            public abstract void Start(MonoBehaviour source);
            public abstract void Stop();
        }

        private class StandardRoutine : Routine
        {
            private IEnumerator m_enumerator;

            protected MonoBehaviour source { get; private set; }
            public override bool isDone { get { return m_enumerator == null; } }

            protected StandardRoutine()
            {
            }

            public StandardRoutine(IEnumerator ie)
            {
                m_enumerator = ie;
            }

            protected void SetRoutine(IEnumerator ie)
            {
                m_enumerator = ie;
            }

            public override void Start(MonoBehaviour source)
            {
                this.source = source;
                source.StartCoroutine(Routine());
            }

            public override void Stop()
            {
                if(isDone)
                    return;

                source.StopCoroutine(m_enumerator);
                m_enumerator = null;
            }

            private IEnumerator Routine()
            {
                yield return source.StartCoroutine(m_enumerator);
                m_enumerator = null;
            }
        }

        private class SequenceRoutine : StandardRoutine
        {
            private readonly List<Routine> m_routines;
            private Routine m_running;

            public SequenceRoutine(params Routine[] routines)
            {
                m_routines = new List<Routine>(routines);
                for(int i = 0; i < m_routines.Count; ++i)
                {
                    SequenceRoutine seq = m_routines[i] as SequenceRoutine;
                    if(seq == null)
                        continue;

                    m_routines.RemoveAt(i);
                    m_routines.InsertRange(i, seq.m_routines);
                    --i;
                }

                SetRoutine(Sequence());
            }

            public override void Stop()
            {
                base.Stop();

                if(m_running != null)
                {
                    m_running.Stop();
                    m_running = null;
                }
            }

            private IEnumerator Sequence()
            {
                foreach(Routine routine in m_routines)
                {
                    m_running = routine;
                    m_running.Start(source);

                    while(!m_running.isDone)
                        yield return null;
                }

                m_running = null;
            }
        }

        public static Routine Sequence(params Routine[] routines)
        {
            if(routines.Length == 1)
                return routines[0];
            return new SequenceRoutine(routines);
        }

        private class ParallelRoutine : Routine
        {
            private readonly List<Routine> m_routines;
            private MonoBehaviour m_source;

            public override bool isDone { get { return m_routines.TrueForAll((Routine r) => r.isDone); } }

            public ParallelRoutine(Routine[] routines)
            {
                m_routines = new List<Routine>(routines);
                for(int i = 0; i < m_routines.Count; ++i)
                {
                    ParallelRoutine parallel = m_routines[i] as ParallelRoutine;
                    if(parallel == null)
                        continue;
                    
                    m_routines.RemoveAt(i);
                    m_routines.AddRange(parallel.m_routines);
                    --i;
                }
            }

            public override void Start(MonoBehaviour source)
            {
                m_source = source;
                m_routines.ForEach((Routine r) => r.Start(m_source));
            }

            public override void Stop()
            {
                m_routines.ForEach((Routine r) => r.Stop());
            }
        }

        public static Routine Parallel(params Routine[] routines)
        {
            if(routines.Length == 1)
                return routines[0];
            return new ParallelRoutine(routines);
        }

        public static Routine Run(IEnumerator enumerator)
        {
            return new StandardRoutine(enumerator);
        }

        private static IEnumerator DelayEnumerator(float delay, bool unscaledTime)
        {
            if(unscaledTime)
                yield return new WaitForSecondsRealtime(delay);
            else
                yield return new WaitForSeconds(delay);
        }

        public static Routine Delay(float delay, bool unscaledTime = false)
        {
            return new StandardRoutine(DelayEnumerator(delay, unscaledTime));
        }

        private class CallRoutine : Routine
        {
            private readonly Action m_action;

            public override bool isDone { get { return true; } }

            public CallRoutine(Action action)
            {
                m_action = action;
            }

            public override void Start(MonoBehaviour source)
            {
                m_action();
            }

            public override void Stop()
            {
            }
        }

        public static Routine Call(Action callback)
        {
            return new CallRoutine(callback);
        }

        public static Routine CallLater(float delay, Action callback)
        {
            return new SequenceRoutine(
                Delay(delay),
                Call(callback));
        }

        public static Routine Activate(bool active, params GameObject[] targets)
        {
            return new CallRoutine(() => Array.ForEach(targets, (GameObject go) => go.SetActive(active)));
        }

        private abstract class EasingRoutine<T> : StandardRoutine
        {
            private readonly Easing.EasingFunction m_easing;
            private readonly float m_step;
            private readonly T m_endValue;
            private readonly bool m_ignoreTimeScale;

            private T m_startValue;
            private float m_startTime;

            protected abstract Func<T, T, float, T> update { get; }
            protected T value { get; private set; }

            protected EasingRoutine(T endValue, float duration, bool ignoreTimeScale, Easing.EaseType easeType)
            {
                m_endValue = endValue;
                m_step = 1f / duration;
                m_easing = Easing.GetEasingFunction(easeType);
                m_ignoreTimeScale = ignoreTimeScale;

                SetRoutine(DoEasing());
            }

            public override void Start(MonoBehaviour source)
            {
                m_startTime = m_ignoreTimeScale ? Time.realtimeSinceStartup : Time.time;
                m_startValue = ReadInitialValue();

                base.Start(source);
            }

            protected abstract T ReadInitialValue();

            private IEnumerator DoEasing()
            {
                float progress = 0f;
                while(progress < 1f)
                {
                    progress = ((m_ignoreTimeScale ? Time.realtimeSinceStartup : Time.time) - m_startTime) * m_step;
                    value = update(m_startValue, m_endValue, m_easing(0f, 1f, Mathf.Clamp01(progress)));
                    Step();
                    yield return null;
                }
            }

            protected abstract void Step();
        }

        private class MoveRoutine : EasingRoutine<Vector3>
        {
            private readonly Transform m_target;
            private readonly bool m_local;

            protected override Func<Vector3, Vector3, float, Vector3> update { get { return Vector3.Lerp; } }

            public MoveRoutine(GameObject target, Vector3 endPosition, float duration, bool ignoreTimeScale, Easing.EaseType easeType, bool local)
                : base(endPosition, duration, ignoreTimeScale, easeType)
            {
                m_target = target.transform;
                m_local = local;
            }

            protected override Vector3 ReadInitialValue()
            {
                return m_local ? m_target.localPosition : m_target.position;
            }

            protected override void Step()
            {
                if(m_local)
                    m_target.localPosition = value;
                else
                    m_target.position = value;
            }
        }

        public static Routine MoveTo(
            GameObject target, Vector3 endPosition, float duration = 1f, bool ignoreTimeScale = false,
            Easing.EaseType easeType = Easing.EaseType.EaseInOutSine, bool isLocal = false)
        {
            return new MoveRoutine(target, endPosition, duration, ignoreTimeScale, easeType, isLocal);
        }

        private class RotateRoutine : EasingRoutine<Quaternion>
        {
            private readonly Transform m_target;
            private readonly bool m_local;

            protected override Func<Quaternion, Quaternion, float, Quaternion> update { get { return Quaternion.Slerp; } }

            public RotateRoutine(GameObject target, Quaternion endRotation, float duration, bool ignoreTimeScale, Easing.EaseType easeType, bool local)
                : base(endRotation, duration, ignoreTimeScale, easeType)
            {
                m_target = target.transform;
                m_local = local;
            }

            protected override Quaternion ReadInitialValue()
            {
                return m_local ? m_target.localRotation : m_target.rotation;
            }

            protected override void Step()
            {
                if(m_local)
                    m_target.localRotation = value;
                else
                    m_target.rotation = value;
            }
        }

        public static Routine RotateTo(
            GameObject target, Quaternion endRotation, float duration = 1f, bool ignoreTimeScale = false,
            Easing.EaseType easeType = Easing.EaseType.EaseInOutSine, bool isLocal = false)
        {
            return new RotateRoutine(target, endRotation, duration, ignoreTimeScale, easeType, isLocal);
        }

        private class ScaleRoutine : EasingRoutine<Vector3>
        {
            private readonly Transform m_target;

            protected override Func<Vector3, Vector3, float, Vector3> update { get { return Vector3.Lerp; } }

            public ScaleRoutine(GameObject target, Vector3 endScale, float duration, bool ignoreTimeScale, Easing.EaseType easeType)
                : base(endScale, duration, ignoreTimeScale, easeType)
            {
                m_target = target.transform;
            }

            protected override Vector3 ReadInitialValue()
            {
                return m_target.localScale;
            }

            protected override void Step()
            {
                m_target.localScale = value;
            }
        }

        public static Routine ScaleTo(
            GameObject target, Vector3 scale, float duration = 1f, bool ignoreTimeScale = false,
            Easing.EaseType easeType = Easing.EaseType.EaseInOutSine)
        {
            return new ScaleRoutine(target, scale, duration, ignoreTimeScale, easeType);
        }

        private class ValueRoutine : EasingRoutine<float>
        {
            private readonly float m_initial;
            private readonly Action<float> m_onUpdate;

            protected override Func<float, float, float, float> update { get { return Mathf.Lerp; } }

            public ValueRoutine(Action<float> onUpdate, float from, float to, float duration, bool ignoreTimeScale, Easing.EaseType easeType)
                : base(to, duration, ignoreTimeScale, easeType)
            {
                m_initial = from;
                m_onUpdate = onUpdate;
            }

            protected override float ReadInitialValue()
            {
                return m_initial;
            }

            protected override void Step()
            {
                m_onUpdate(value);
            }
        }

        public static Routine ValueTo(
            Action<float> onUpdate, float from, float to, float duration = 1f, bool ignoreTimeScale = false,
            Easing.EaseType easeType = Easing.EaseType.EaseInOutSine)
        {
            return new ValueRoutine(onUpdate, from, to, duration, ignoreTimeScale, easeType);
        }

        private class EasingDerivativeRoutine : StandardRoutine
        {
            private readonly Action<float> m_onUpdate;
            private readonly float m_start;
            private readonly float m_end;
            private readonly Easing.EasingFunction m_easing;
            private readonly float m_step;
            private readonly bool m_ignoreTimeScale;

            private float m_startTime;

            public EasingDerivativeRoutine(Action<float> onUpdate, float from, float to, float duration, bool ignoreTimeScale, Easing.EaseType easeType)
            {
                m_onUpdate = onUpdate;
                m_start = from;
                m_end = to;
                m_step = 1f / duration;
                m_easing = Easing.GetEasingDerivativeFunction(easeType);
                m_ignoreTimeScale = ignoreTimeScale;

                SetRoutine(DoEasing());
            }

            public override void Start(MonoBehaviour source)
            {
                m_startTime = m_ignoreTimeScale ? Time.realtimeSinceStartup : Time.time;
                base.Start(source);
            }

            private IEnumerator DoEasing()
            {
                float progress = 0f;
                while(progress < 1f)
                {
                    progress = ((m_ignoreTimeScale ? Time.realtimeSinceStartup : Time.time) - m_startTime) * m_step;
                    float value = Mathf.LerpUnclamped(m_start, m_end, m_easing(0f, 1f, progress));
                    m_onUpdate(value);
                    yield return null;
                }
            }
        }

        public static Routine DerivativeValueTo(
            Action<float> onUpdate, float from, float to, float duration = 1f, bool ignoreTimeScale = false,
            Easing.EaseType easeType = Easing.EaseType.EaseInOutSine)
        {
            return new EasingDerivativeRoutine(onUpdate, from, to, duration, ignoreTimeScale, easeType);
        }

        private static IEnumerator WhileEnumerator(Func<bool> predicate)
        {
            while(predicate())
                yield return null;
        }

        public static Routine While(Func<bool> predicate)
        {
            return new StandardRoutine(WhileEnumerator(predicate));
        }

        public static Routine WaitForState(Animator animator, string stateName, int layerIndex = 0)
        {
            return new StandardRoutine(
                WhileEnumerator(() => !animator.GetCurrentAnimatorStateInfo(layerIndex).IsName(stateName))
            );
        }

        private class DeferredCreationRoutine : Routine
        {
            private readonly Func<Routine> m_creator;

            private Routine m_inner;

            public override bool isDone { get { return m_inner != null && m_inner.isDone; } }

            public DeferredCreationRoutine(Func<Routine> creator)
            {
                m_creator = creator;
            }

            public override void Start(MonoBehaviour source)
            {
                m_inner = m_creator.Invoke();
                m_inner.Start(source);
            }

            public override void Stop()
            {
                if(m_inner != null)
                    m_inner.Stop();
            }
        }

        public static Routine Deferred(Func<Routine> creator)
        {
            return new DeferredCreationRoutine(creator);
        }
    }
}
