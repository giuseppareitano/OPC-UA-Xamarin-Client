﻿using OPC_UA_Client.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OPC_UA_Client.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SubscriptionsPage : ContentPage
    {
        ClientOPC client;
        ObservableCollection<SubscriptionView> subscriptionsView = new ObservableCollection<SubscriptionView>();
        List<SubscriptionView> storedList;

        public SubscriptionsPage(ClientOPC _client)
        {
            client = _client;
            storedList = _client.GetSubscriptionViews();
            InitializeComponent();
            BindingContext = subscriptionsView;
            displaySubscriptions();
        }

        private void displaySubscriptions()
        {
            subscriptionsView.Clear();
            foreach (var sub in storedList)
            {

                subscriptionsView.Add(sub);

            }
            SubscriptionsDisplay.ItemsSource = null;
            SubscriptionsDisplay.SeparatorColor = Color.FromHex("#4CAF50");
            SubscriptionsDisplay.ItemsSource = subscriptionsView;
        }

        private async void OnTapped(object sender, ItemTappedEventArgs e)
        {
            var item = e.Item as SubscriptionView;
            ContentPage detailSubPage = new DetailSubscriptionPage(client, item.SubscriptionID);
            detailSubPage.Title = "OPC Subscription Details";
            await Navigation.PushAsync(detailSubPage);
        }
    }
}