﻿using Rg.Plugins.Popup.Services;
using System.Linq;
using Xamarin.Forms;

namespace ride.Behaviors
{
    public class Blistapaises: Behavior<View>
    {
        double Verticaltransition {  get; set; }
        PanGestureRecognizer panGesture = new PanGestureRecognizer();
        protected override void OnAttachedTo(View vista)
        {
            panGesture.PanUpdated += PanGesture_PanUpdated;
            vista.GestureRecognizers.Add(panGesture);
            base.OnAttachedTo(vista);

            }
        protected override void OnDetachingFrom(View vista)
            {
            panGesture.PanUpdated-=PanGesture_PanUpdated;
            vista.GestureRecognizers.Remove(panGesture);
            base.OnDetachingFrom(vista);
            }

        private async void PanGesture_PanUpdated(object sender, PanUpdatedEventArgs e)
        {
            switch (e.StatusType)
            {
                case GestureStatus.Running:
                    if (e.TotalY > 0)
                    {
                        await ((View)sender).TranslateTo(0, e.TotalY);
                        Verticaltransition = e.TotalY;
                    }
                    break;
                case GestureStatus.Completed:
                    if (Verticaltransition > 80)
                    {
                        await ((View)sender).TranslateTo(0, 200);
                        if (PopupNavigation.Instance.PopupStack.Any())
                        {
                            await PopupNavigation.Instance.PopAsync();
                        }
                    }
                    else
                    {
                        await ((View)sender).TranslateTo(0, e.TotalY);
                    }
                    break;
            }
        }

    }
}
