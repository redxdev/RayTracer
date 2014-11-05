using RTLib.Util;

namespace RTLib.Render.Targets
{
    public interface IRenderTarget
    {
        void StartRender(Renderer renderer);

        void FinishRender(Renderer renderer);

        void PixelRendered(Renderer renderer, int x, int y, Color color);
    }
}
