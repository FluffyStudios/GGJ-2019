using System.Collections;
using UnityEngine;

namespace FluffyBox
{
    public class Application : UnityEngine.MonoBehaviour
    {
        public UnityEngine.GameObject Bootstrapper;
        public UnityEngine.GameObject GameRoot;
        public UnityEngine.GameObject SkyboxCamera;
        public float MusicVolume = 0.75f;
        public AudioClip AccusationTheme;

        private static Application instance;
        public static Application Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = UnityEngine.GameObject.FindObjectOfType(typeof(Application)) as Application;
                }

                return instance;
            }
        }
        
        public static string KeyMapsPath
        {
            get
            {
                return UnityEngine.Application.dataPath + "/Resources/KeyMaps";
            }
        }

        public string Version;
        
        private Manager[] managers;

        private void Awake()
        {
            this.managers = this.GetComponentsInChildren<Manager>();
        }

        private void Start()
        {
            this.StartCoroutine(this.Ignite());
        }

        private IEnumerator Ignite()
        {
            this.Bootstrapper.SetActive(true);

            for (int i = 0, lth = this.managers.Length; i < lth; ++i)
            {
                this.StartCoroutine(managers[i].Ignite());
            }

            for (int i = 0, lth = this.managers.Length; i < lth; ++i)
            {
                while (!this.managers[i].Alive)
                {
                    yield return null;
                }
            }

            this.OnIgnitionCompleted();
            yield break;
        }

        private void OnIgnitionCompleted()
        {
            for (int i = 0, lth = this.managers.Length; i < lth; ++i)
            {
                this.managers[i].OnIgnitionCompleted();
            }

            this.Bootstrapper.SetActive(false); 

        }

        private void OnDestroy()
        {
            Services.ClearServices();
        }
    }
}