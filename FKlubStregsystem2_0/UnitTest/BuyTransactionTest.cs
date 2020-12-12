using System;
using System.Collections.Generic;
using System.Transactions;
using FKlubStregsystem;
using NUnit.Framework;

namespace BuyTransactionTests
{ 
    public class BuyTransactionTest
    {



        //Krav:ID
        //En transaktion har et unikt ID. Det er vigtigt at kunne skelne
        //transaktioner fra hinanden.
        //Dette ID bestemmer rækkefølgen af transaktioner.
        [TestCase("should sort transactions in the order they were created")]
        public void ID_isGenerated_ShouldBeOrderedByTimeCreated (string desc)
        {
            //arrange
            User user = new User();
            Product A = new Product(id: 1, name: "A ", price: 10);
            Product B = new Product(id: 2, name: "B ", price: 100);
            Product C = new Product(id: 3, name: "C ", price: 10);
            Product D = new Product(id: 4, name: "D ", price: 100);

            //TODO find en bedre test!!
            BuyTransaction tr1 = new BuyTransaction(user, A);
            System.Threading.Thread.Sleep(50);
            BuyTransaction tr2 = new BuyTransaction(user, B);
            System.Threading.Thread.Sleep(50);
            BuyTransaction tr3 = new BuyTransaction(user, C);
            System.Threading.Thread.Sleep(50);
            BuyTransaction tr4 = new BuyTransaction(user, D);
            
            List<BuyTransaction> expectedList = new List<BuyTransaction>() { tr4, tr3, tr2, tr1 };
            string expected = "";
            foreach (BuyTransaction item in expectedList)
            {
                expected += $"\n{item.Product.Name}{item.ID}";
            }

            List<BuyTransaction> actualList = new List<BuyTransaction>() { tr2, tr1, tr3, tr4 };
            string actual = "";

            //act
            actualList.Sort();
            foreach(BuyTransaction item in actualList)
            {
                actual += $"\n{item.Product.Name}{item.ID}";
            }

            //assert
            Assert.AreEqual(expectedList,actualList, $"desc: \nactual:{actual}\nexpected:{expected}");
        }


        //krav:User
        //Den bruger der er en del af transaktionen. Denne må ikke være null.
        [TestCase("Exception Should be  thrown when user is set to null")]
        public void User_SetToNull_ShouldThrowException(string desc)
        {
            //arrange
            User user = new User();
            Product apple = new Product(id: 1, name: "apple", price: 10);
            Product banana = new Product(id: 2, name: "banana", price: 100);

            //act

            //assert
            Assert.Throws<ArgumentNullException>(() => new BuyTransaction(null, apple), desc);
        }



        //krav: Date
        //Dato for transaktion. Denne dato skal så vidt muligt være korrekt!
        //det kan ikke rigtigt testes, hvad betyder korrekt mon?
        //Jeg har besluttet at det betyder at Dato skal sættes til utcNow når
        //en ny transaktion oprettes, Dette gælder ikke når en transaktion hentes fra en log.
        //Altså to forskellige constructors.
        //ikke testet


        //krav: Amount
        //Amount er prisen på produktet på købstidspunktet. Produkter ændrer
        //pris over tid, men ikke med tilbagevirkende kraft!
        //Amount svarer til et produkts pris*(-1) på købstidspunktet
        //ikke testet



        //krav: ToString
        //Denne transactiontype har en specialiseret ToString der fortæller
        //at der er tale om:
        //et køb,beløb, bruger, produkt, hvornår købet blev foretaget, og transaktionens id.
        //(du bestemmer rækkefølge og detaljer)
        [TestCase("ToString should return string(Date;TransactID;Buy;user;Product;Amount)")]
        public void ToString_BuyTransAction_ShouldReturnString(string desc)
        {
            //arrange
            User user = new User();
            Product A = new Product(id: 1, name: "A ", price: 10);
            DateTime date = Convert.ToDateTime("1 / 1 / 2020");
            Guid id = Guid.NewGuid();
            decimal amount = 20;
            BuyTransaction tr = new BuyTransaction(user: user, product: A, date: date, id: id, amount: amount);

            string expected = $"{date};{id};Buy;{user};{A};{amount}";
            //act
            string actual = tr.ToString(); 

            //assert
            Assert.AreEqual(expected, actual, desc);
        }

        //krav: Execute
        //Trækker beløbet fra brugerens konto. Der opstår en fejlsituation
        //hvis brugeren ikke har nok penge på sin konto og produktet, der
        //købes, ikke tillader overtræk. Til dette formål defineres en exception.
        //InsufficientCreditsException
        [TestCase("Exception Should be  thrown when credit are insuficient for the purchase")]
        public void BuyTransaction_buyingSomethingYouCantAfford_ShouldThrowInsufficientCreditsException(string desc)
        {
            //arrange
            User user = new User(id: 1, firstname: "jakob1", lastname: "skov", username: "js1", email: "saxjax@saxjax.dk", balance: 100, lowBalanceDefinition: 50);
            Product apple = new Product(id: 1, name: "apple", price: 199);
            BuyTransaction tr = new BuyTransaction(user: user, product: apple);

            //act

            //assert
            Assert.Throws<InsufficientCreditsException>(() => tr.Execute(), desc);
        }


        [TestCase("Exception Should be  thrown when trying to buy an inactive product")]
        public void BuyTransaction_buyingInactiveProduct_ShouldThrowProductNotActivatedException(string desc)
        {
            //arrange
            User user = new User(id: 1, firstname: "jakob1", lastname: "skov", username: "js1", email: "saxjax@saxjax.dk", balance: 100, lowBalanceDefinition: 50);
            Product apple = new Product(id: 1, name: "apple", price: 10, active: false);
            BuyTransaction tr = new BuyTransaction(user: user, product: apple);
            //act

            //assert
            Assert.Throws<ProductNotActivatedException>(() =>tr.Execute() , desc);
        }


        [TestCase("When a product is bought the users balance should be debited with the productPrice")]
        public void BuyTransaction_buyingProduct_ShouldWithdrawAmountFromUserBalance(string desc)
        {
            //arrange
            User user = new User(id: 1, firstname: "jakob1", lastname: "skov", username: "js1", email: "saxjax@saxjax.dk", balance: 100, lowBalanceDefinition: 50);
            Product apple = new Product(id: 1, name: "apple", price: 10, active: true);
            BuyTransaction tr = new BuyTransaction(user, apple);
            decimal expected = 90;
            //act
            tr.Execute();

            //assert
            Assert.AreEqual(expected,user.Balance, $"{desc}: actual:{tr.User.Balance},expected:{expected}");
        }









    }
}
