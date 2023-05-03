//using System;
//using Microsoft.Maui.Controls.Compatibility;
//using Microsoft.Maui.Controls.Handlers.Compatibility;
//using Plitkarka.CustomElements;

//[assembly: ExportRenderer(typeof(Shell), typeof(CustomShellRenderer))]
//namespace Plitkarka.CustomElements
//{
//    public class CustomShellRenderer : ShellRenderer
//    {
//        protected override IShellPageRendererTracker CreatePageRendererTracker()
//        {
//            return new CustomShellPageRendererTracker(this);
//        }
//    }

//    public class CustomShellPageRendererTracker : ShellPageRendererTracker
//    {
//        public CustomShellPageRendererTracker(IShellContext context)
//            : base(context)
//        {

//        }

//        protected override void UpdateTitleView()
//        {
//            if (ViewController == null || ViewController.NavigationItem == null)
//                return;

//            var titleView = Shell.GetTitleView(Page);

//            if (titleView == null)
//            {
//                var view = ViewController.NavigationItem.TitleView;
//                ViewController.NavigationItem.TitleView = null;
//                view?.Dispose();
//            }
//            else
//            {
//                var view = new CustomTitleViewContainer(titleView);
//                ViewController.NavigationItem.TitleView = view;
//            }
//        }
//    }

//    public class CustomTitleViewContainer : UIContainerView
//    {
//        public CustomTitleViewContainer(View view) : base(view)
//        {
//            TranslatesAutoresizingMaskIntoConstraints = false;
//        }

//        public override CGSize IntrinsicContentSize => UILayoutFittingExpandedSize;
//    }
//}

