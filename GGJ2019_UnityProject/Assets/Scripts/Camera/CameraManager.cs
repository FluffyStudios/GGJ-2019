using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : FluffyBox.Manager, ICameraService
{
    public Camera MainCamera;
    public Animator MainCameraAnimator;
    public AudioSource AudioSource;
    public AudioClip ZoomClip;
    public AudioClip UnzoomClip;

    private CameraState privateCurrentState;

    public enum CameraState
    {
        Inactive,
        MainMenu,
        Transition,
        InGame
    }

    public CameraState CurrentState
    {
        get
        {
            return this.privateCurrentState;
        }

        private set
        {
            if (this.privateCurrentState != value)
            {
                Debug.Log(string.Format("Camera State Changed: {0} => {1}", this.privateCurrentState.ToString(), value.ToString()));
                this.privateCurrentState = value;
            }
        }
    }

    public void Zoom(bool zoomIn)
    {
        this.CurrentState = CameraState.Transition;
        this.MainCameraAnimator.SetBool("ZoomIn", zoomIn);
        this.AudioSource.PlayOneShot(zoomIn ? this.ZoomClip : this.UnzoomClip);
    }

    public void ZoomInEnded()
    {
        this.CurrentState = CameraState.InGame;
    }

    public void ZoomOutEnded()
    {
        this.CurrentState = CameraState.MainMenu;
    }

    public override void RegisterService()
    {
        FluffyBox.Services.AddService<ICameraService>(this);
    }

    public override IEnumerator Ignite()
    {
        yield return base.Ignite();

        this.CurrentState = CameraState.Inactive;
    }

    public override void OnIgnitionCompleted()
    {
        this.CurrentState = CameraState.MainMenu;
    }
}
