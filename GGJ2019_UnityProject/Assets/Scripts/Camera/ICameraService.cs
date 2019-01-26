public interface ICameraService : FluffyBox.IService
{
    CameraManager.CameraState CurrentState
    {
        get;
    }

    void Zoom(bool zoomIn);
}
