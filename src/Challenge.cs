#:property PublishAot=false

// DESAFIO: Sistema de Notificações Multi-Plataforma
// PROBLEMA: Um aplicativo precisa exibir notificações em diferentes plataformas (Web, Mobile, Desktop)
// com diferentes tipos de conteúdo (Texto, Imagem, Vídeo). O código atual cria uma explosão de classes
// combinando cada tipo de notificação com cada plataforma

using System;

namespace DesignPatternChallenge
{
    public interface INotificationRenderer
    {
        void RenderText(string title, string content);
        void RenderImage(string title, string content, string imageUrl);
        void RenderVideo(string title, string content, string videoUrl);
    }

    public class WebRenderer : INotificationRenderer
    {
        public void RenderText(string title, string content)
        {
            Console.WriteLine($"[Web - HTML] <div class='notification'>");
            Console.WriteLine($"  <h3>{title}</h3>");
            Console.WriteLine($"  <p>{content}</p>");
            Console.WriteLine("</div>");
        }

        public void RenderImage(string title, string content, string imageUrl)
        {
            Console.WriteLine($"[Web - HTML] <div class='notification-image'>");
            Console.WriteLine($"  <img src='{imageUrl}' />");
            Console.WriteLine($"  <h3>{title}</h3>");
            Console.WriteLine($"  <p>{content}</p>");
            Console.WriteLine("</div>");
        }

        public void RenderVideo(string title, string content, string videoUrl)
        {
            Console.WriteLine($"[Web - HTML] <div class='notification-video'>");
            Console.WriteLine($"  <video src='{videoUrl}' controls></video>");
            Console.WriteLine($"  <h3>{title}</h3>");
            Console.WriteLine($"  <p>{content}</p>");
            Console.WriteLine("</div>");
        }
    }

    public class MobileRenderer : INotificationRenderer
    {
        public void RenderText(string title, string content)
        {
            Console.WriteLine($"[Mobile - Native] Push Notification:");
            Console.WriteLine($"Title: {title}");
            Console.WriteLine($"Body: {content}");
            Console.WriteLine($"Icon: notification_icon.png");
        }

        public void RenderImage(string title, string content, string imageUrl)
        {
            Console.WriteLine($"[Mobile - Native] Rich Push Notification:");
            Console.WriteLine($"Title: {title}");
            Console.WriteLine($"Body: {content}");
            Console.WriteLine($"Image: {imageUrl}");
            Console.WriteLine($"Style: BigPictureStyle");
        }

        public void RenderVideo(string title, string content, string videoUrl)
        {
            Console.WriteLine($"[Mobile - Native] Video Push Notification:");
            Console.WriteLine($"Title: {title}");
            Console.WriteLine($"Body: {content}");
            Console.WriteLine($"Video: {videoUrl}");
            Console.WriteLine($"Action: Tap to play");
        }
    }

    public class DesktopRenderer : INotificationRenderer
    {
        public void RenderText(string title, string content)
        {
            Console.WriteLine($"[Desktop - Toast] Windows Notification:");
            Console.WriteLine($"╔══════════════════════════╗");
            Console.WriteLine($"║ {title.PadRight(24)} ║");
            Console.WriteLine($"║ {content.PadRight(24)} ║");
            Console.WriteLine($"╚══════════════════════════╝");
        }

        public void RenderImage(string title, string content, string imageUrl)
        {
            Console.WriteLine($"[Desktop - Toast] Windows Notification with Image:");
            Console.WriteLine($"╔══════════════════════════╗");
            Console.WriteLine($"║ [IMG: {imageUrl.Substring(0, Math.Min(15, imageUrl.Length))}...]  ║");
            Console.WriteLine($"║ {title.PadRight(24)} ║");
            Console.WriteLine($"║ {content.PadRight(24)} ║");
            Console.WriteLine($"╚══════════════════════════╝");
        }

        public void RenderVideo(string title, string content, string videoUrl)
        {
            Console.WriteLine($"[Desktop - Toast] Windows Notification with Video:");
            Console.WriteLine($"╔══════════════════════════╗");
            Console.WriteLine($"║ ▶ {videoUrl.Substring(0, Math.Min(20, videoUrl.Length))}... ║");
            Console.WriteLine($"║ {title.PadRight(24)} ║");
            Console.WriteLine($"║ {content.PadRight(24)} ║");
            Console.WriteLine($"╚══════════════════════════╝");
        }
    }

    public abstract class Notification
    {
        protected readonly INotificationRenderer Renderer;
        protected readonly string Title;
        protected readonly string Content;

        protected Notification(INotificationRenderer renderer, string title, string content)
        {
            Renderer = renderer;
            Title = title;
            Content = content;
        }

        public abstract void Show();
    }

    public class TextNotification : Notification
    {
        public TextNotification(INotificationRenderer renderer, string title, string content)
            : base(renderer, title, content) { }

        public override void Show() => Renderer.RenderText(Title, Content);
    }

    public class ImageNotification : Notification
    {
        private readonly string _imageUrl;

        public ImageNotification(INotificationRenderer renderer, string title, string content, string imageUrl)
            : base(renderer, title, content)
        {
            _imageUrl = imageUrl;
        }

        public override void Show() => Renderer.RenderImage(Title, Content, _imageUrl);
    }

    public class VideoNotification : Notification
    {
        private readonly string _videoUrl;

        public VideoNotification(INotificationRenderer renderer, string title, string content, string videoUrl)
            : base(renderer, title, content)
        {
            _videoUrl = videoUrl;
        }

        public override void Show() => Renderer.RenderVideo(Title, Content, _videoUrl);
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Sistema de Notificações Multi-Plataforma ===\n");

            INotificationRenderer web = new WebRenderer();
            INotificationRenderer mobile = new MobileRenderer();
            INotificationRenderer desktop = new DesktopRenderer();

            Notification textWeb = new TextNotification(web, "Novo Pedido", "Você tem um novo pedido");
            textWeb.Show();
            Console.WriteLine();

            Notification textMobile = new TextNotification(mobile, "Novo Pedido", "Você tem um novo pedido");
            textMobile.Show();
            Console.WriteLine();

            Notification imageWeb = new ImageNotification(
                web,
                "Promoção", 
                "50% de desconto!", 
                "promo.jpg"
            );
            imageWeb.Show();
            Console.WriteLine();

            Notification videoMobile = new VideoNotification(
                mobile,
                "Tutorial", 
                "Aprenda a usar o app", 
                "tutorial.mp4"
            );
            videoMobile.Show();
            Console.WriteLine();

            Notification videoDesktop = new VideoNotification(
                desktop,
                "Treinamento",
                "Veja o passo a passo",
                "treinamento.mp4");
            videoDesktop.Show();
            Console.WriteLine();

            Console.WriteLine("=== BRIDGE APLICADO ===");
            Console.WriteLine("✓ Tipo da notificação desacoplado da plataforma");
            Console.WriteLine("✓ Novos tipos sem alterar renderizadores");
            Console.WriteLine("✓ Novas plataformas sem alterar tipos");
            Console.WriteLine("✓ Sem explosão combinatória de classes");
        }
    }
}
