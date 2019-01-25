using System.Collections;
using FluffyBox.Extensions;
using UnityEngine;
using UnityEngine.UI;

public class MainGameScreen : FluffyBox.GuiScreen
{
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


    protected override IEnumerator OnBeginShow(bool animated = true)
    {
        yield return base.OnBeginShow(animated);

        this.GameService = FluffyBox.Services.GetService<IGameService>();
    }

    protected override void OnEndHide()
    {
        base.OnEndHide();
        
        this.GameService = null;
    }
}
 