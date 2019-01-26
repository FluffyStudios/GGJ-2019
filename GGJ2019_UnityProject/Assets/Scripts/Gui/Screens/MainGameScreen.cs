using System.Collections;
using FluffyBox.Extensions;
using UnityEngine;
using UnityEngine.UI;

public class MainGameScreen : FluffyBox.GuiScreen
{
    public Animator TitleAnimator;

    private IGameService privateGameService;

    private IGameService GameService
    {
        get
        {
            return this.privateGameService;
        }

        set
        {
            this.privateGameService = value;
        }
    }

    public void OnZoomCb()
    {
        ICameraService cameraService = FluffyBox.Services.GetService<ICameraService>();
        if (cameraService.CurrentState != CameraManager.CameraState.MainMenu)
        {
            return;
        }

        cameraService.Zoom(true);
        this.TitleAnimator.SetBool("Exit", true);
    }

    public void OnZoomOutFinished()
    {
    }

    public void OnZoomInFinished()
    {
        Gui.GuiService.HideWindow<MainGameScreen>(false);
        Gui.GuiService.ShowWindow<InGameScreen>();
        PlanetManager.Instance.StartMissionAnim();
    }
    
    protected override IEnumerator OnBeginShow(bool animated = true)
    {
        yield return base.OnBeginShow(animated);

        this.GameService = FluffyBox.Services.GetService<IGameService>();

        this.TitleAnimator.SetBool("Exit", false);
    }

    protected override void OnEndHide()
    {
        base.OnEndHide();
        
        this.GameService = null;
    }
}
 