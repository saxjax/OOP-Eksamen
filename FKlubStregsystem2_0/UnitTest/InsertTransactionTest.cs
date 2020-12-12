using System;
using System.Collections.Generic;
using System.Transactions;
using FKlubStregsystem;
using NUnit.Framework;

namespace InsertTransactionTests
{ 
    public class InsertTransactionTest
    {

        //krav: ToString
        //Denne transactionstype har en specialiseret ToString, der fortæller
        //at der er tale om
        //en indbetaling, beløb, bruger, og hvornår indbetalingen blev foretaget
        //og id på transaktionen. (du bestemmer rækkefølge og detaljer)
        
        [TestCase("ToString should return string(Date;TransactID;Insert;user;Amount)")]
        public void ToString_BuyTransAction_ShouldReturnString(string desc)
        {
            //arrange
            User user = new User();
            DateTime date = Convert.ToDateTime("1 / 1 / 2020");
            Guid id = Guid.NewGuid();
            decimal amount = 20;
            InsertCashTransaction tr = new InsertCashTransaction(user: user, date: date, id: id, amount: amount);

            string expected = $"{date};{id};Insert;{user};{amount}";
            //act
            string actual = tr.ToString(); 

            //assert
            Assert.AreEqual(expected, actual, desc);
        }

        //krav: Execute
        //Lægger beløbet til brugerens konto.
        [TestCase(100,10,"Balance should be added the amount deposited, and subtracted if amount is negative")]
        [TestCase(100, -10, "Balance should be added the amount deposited, and subtracted if amount is negative")]
        [TestCase(100, -100, "Balance should be added the amount deposited, and subtracted if amount is negative")]
        [TestCase(100, -1000, "Balance should be added the amount deposited, and subtracted if amount is negative")]

        public void InsertTransaction_InsertingAmountOnUsersBalance_ShouldUpdateBalance(decimal balance, decimal amount, string desc)
        {
            //arrange
            User user = new User(id: 1, firstname: "jakob1", lastname: "skov", username: "js1", email: "saxjax@saxjax.dk", balance: balance, lowBalanceDefinition: 50);
            InsertCashTransaction tr = new InsertCashTransaction(user, amount);
            decimal expected = balance + amount;
            //act
            tr.Execute();

            //assert
            Assert.AreEqual(expected,tr.User.Balance, $"{desc}: actual:{tr.User.Balance} expected:{expected}");
        }



    }
}
