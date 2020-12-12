using System;
using System.Collections.Generic;
using FKlubStregsystem;
using NUnit.Framework;


namespace StregsystemCLITests
{
    public class StregsystemCLITest
    {
        //Krav:Start
        //En metode der starter brugergrænsefladen.
        //TODO UI starter men test virker ikke

        //[TestCase("Start UserInterface!")]
        //public void Start_NoInput_ShouldStartUserInterface(string desc)
        //{
        //    //arrange
        //    StregsystemCLI CLI = new StregsystemCLI();
        //    //create userDB
        //    decimal LowBalanceDefinition = 50;
        //    User user1 = new User(id: 1, firstname: "jakob1", lastname: "skov", username: "js1", email: "saxjax@saxjax.dk", balance: 100, lowBalanceDefinition: LowBalanceDefinition);
        //    User user2 = new User(id: 20, firstname: "jakob20", lastname: "skov", username: "js2", email: "saxjax@saxjax.dk", balance: 10, lowBalanceDefinition: LowBalanceDefinition);
        //    User user3 = new User(id: 3, firstname: "jakob3", lastname: "skov", username: "js3", email: "saxjax@saxjax.dk", balance: 25, lowBalanceDefinition: LowBalanceDefinition);
        //    User user4 = new User(id: 4, firstname: "jakob4", lastname: "skov", username: "js4", email: "saxjax@saxjax.dk", balance: 1000, lowBalanceDefinition: LowBalanceDefinition);
        //    User user5 = new User(id: 50, firstname: "jakob50", lastname: "skov", username: "js5", email: "saxjax@saxjax.dk", balance: 100, lowBalanceDefinition: LowBalanceDefinition);
        //    User user6 = new User(id: 6, firstname: "jakob6", lastname: "skov", username: "js6", email: "saxjax@saxjax.dk", balance: 100, lowBalanceDefinition: LowBalanceDefinition);

        //    List<User> actualUserList = new List<User>() { user1, user2, user3, user4, user5, user6 };


        //    //create productDB
        //    Product p1 = new Product(id: 1, name: "a", price: 100, active: true, canBeBoughtOnCredit: false);
        //    SeasonalProduct p2 = new SeasonalProduct(id: 10, name: "b", price: 1, active: true, canBeBoughtOnCredit: false, seasonStartDate: "2020-1-24", seasonEndDate: "2020-12-24");
        //    Product p3 = new Product(id: 11, name: "c", price: 2, active: true, canBeBoughtOnCredit: false);
        //    Product p4 = new Product(id: 2, name: "d", price: 25, active: true, canBeBoughtOnCredit: false);
        //    Product p5 = new Product(id: 5, name: "e", price: 1000000, active: false, canBeBoughtOnCredit: false);

        //    List<Product> actualProdList = new List<Product>() { p1, p2, p3, p4, p5 };

        //    CLI.Stregsystem = new Stregsystem(actualProdList, actualUserList);


        //    //act
        //    CLI.Start(":q");
        //    bool actual = CLI.Running;

        //    //assert
        //    Assert.IsTrue(CLI.Running, desc);
        //}

        //Brugergrænsefladen skal kun vise aktive produkter.
        //Ingen test


        //Denne klasse er den eneste i systemet der må skrive noget ud til brugeren!
        //Ingen test


        //krav: Når brugergrænsefladen er startet, vil menuen blive vist, og være
        //klar til at modtage quickbuy kommandoer.
        //Ingen test


       




    }
}
