using System;
using System.Collections.Generic;
using FKlubStregsystem;
using NUnit.Framework;

namespace UserTests
{
    public class UserTest
    {
        [SetUp]
        public void Setup()
        {
        }

        

        [TestCase("Exception Should be  thrown when Firstname is set to null")]
        public void FirstName_SetToNull_ShouldThrowException(string desc)
        {
            //arrange
            User user = new User() { FirstName = "1234" };

            //act

            //assert
            Assert.Throws<ArgumentNullException>(() => user.FirstName = null, desc);
        }

        [TestCase("Exception Should be thrown when Lasttname is set to null")]
        public void LastName_SetToNull_ShouldThrowException(string desc)
        {
            //arrange
            User user = new User() { LastName = "1234" };

            //act

            //assert
            Assert.Throws<ArgumentNullException>(() => user.LastName = null, desc);
        }


        //Brugernavnet må indeholde 0- 9, små bogstaver og underscore: [0-9], [a-z], og '_' 
        [TestCase("cde_123", "cde_123", "Username should be changed to when form is correct")]
        public void UserName_String_ShouldBeSetWhenUserNameIsValid(string expected,string username, string desc)
        {
            //arrange
            User user = new User();
            user.UserName = "123";
            //act
            user.UserName = username;

            //assert
            Assert.AreEqual(expected,user.UserName, desc);
        }

        //Brugernavnet må indeholde 0- 9, små bogstaver og underscore: [0-9], [a-z], og '_' 
        [TestCase( ".", "Throw exception when usename \".\" is invalid")]
        [TestCase("A", "Throw exception when usename  \"A\"is invalid")]
        [TestCase("-", "Throw exception when usename  \"a-b\"is invalid")]
        public void UserName_String_ShouldThrowExceptionWhenFormatIsBad(string username ,string desc)
        {
            //arrange
            User user = new User();

            //act

            //assert
            Assert.Throws<FormatException>(() => user.UserName = username, desc);
        }


        //Brugernavnet må indeholde 0- 9, små bogstaver og underscore: [0-9], [a-z], og '_' 
        [TestCase("cde_123", "Should not Throw exception when usename \"cde_123\" is valid")]
        public void UserName_String_ShouldNotThrowExceptionWhenFormatIsValid(string username, string desc)
        {
            //arrange
            User user = new User();

            //act

            //assert
            Assert.DoesNotThrow(() => user.UserName = username, desc);
        }



        ///Email
        ///Email localpart  a-z, A-Z, og tallene 0-9 samt tegnene punktum, underscore og bindestreg ('.', '_' og '-')
        ///Email Hostpartdomain må indeholde a-z, A-Z, og tallene 0-9 samt punktum og bindestreg. domain må ikke starte eller slutte med bindestreg/punktum. domain skal indeholde mindst et punktum.
        ///• Et eksempel på en gyldig email-adresse er: eksempel @domain.dk
        ///• Et eksempel på en ugyldig email-adresse er: eksempel(2)@-mit_domain.dk
        [TestCase("cde.ABC1-2_3@Z.z-1", "cde.ABC1-2_3@Z.z-1", "Email should be changed to \"cde.ABC1 - 2_3@Z.z - 1\" when form is correct")]
        public void Email_String_ShouldBeSetWhenUserNameIsValid(string expected, string email, string desc)
        {
            //arrange
            User user = new User();
            //act
            user.Email = email;

            //assert
            Assert.AreEqual(expected, user.Email, desc);
        }


        ///Email localpart  a-z, A-Z, og tallene 0-9 samt tegnene punktum, underscore og bindestreg ('.', '_' og '-')
        ///Email Hostpartdomain må indeholde a-z, A-Z, og tallene 0-9 samt punktum og bindestreg. domain må ikke starte eller slutte med bindestreg/punktum. domain skal indeholde mindst et punktum.
        ///• Et eksempel på en gyldig email-adresse er: eksempel @domain.dk
        ///• Et eksempel på en ugyldig email-adresse er: eksempel(2)@-mit_domain.dk        [TestCase(".", "Throw exception when usename \".\" is invalid")]
        [TestCase("a.b@a.b.", "Throw exception when email  \"a.b@a.b.\"is invalid")]
        [TestCase("a-_", "Throw exception when email  \"-\"is invalid")]
        public void Email_String_ShouldThrowExceptionWhenFormatIsBad(string email, string desc)
        {
            //arrange
            User user = new User();

            //act

            //assert
            Assert.Throws<FormatException>(() => user.Email = email, desc);
        }


        ///Email localpart  a-z, A-Z, og tallene 0-9 samt tegnene punktum, underscore og bindestreg ('.', '_' og '-')
        ///Email Hostpartdomain må indeholde a-z, A-Z, og tallene 0-9 samt punktum og bindestreg. domain må ikke starte eller slutte med bindestreg/punktum. domain skal indeholde mindst et punktum.
        ///• Et eksempel på en gyldig email-adresse er: eksempel @domain.dk
        ///• Et eksempel på en ugyldig email-adresse er: eksempel(2)@-mit_domain.dk
        [TestCase("cde.ABC1-2_3@Z.z-1", "Throw exception when email  \"a.b@a.b\"is invalid")]
        public void Email_String_ShouldNotThrowExceptionWhenFormatIsValid(string email, string desc)
        {
            //arrange
            User user = new User();

            //act

            //assert
            Assert.DoesNotThrow(() => user.Email = email, desc);
        }

        //Krav :• Klassen skal implementere IComparable, og sorteres på ID.
        //Brugere
        [TestCase("Test if sort is sorting by userid ")]
        public void SortingIsImplemented_ShouldReturnListSortedDescendingByID(string desc)
        {
            //arrange
            List<User> actualList = new List<User>();
            User user1 = new User(id: 1, firstname: "jakob1", lastname: "skov", username: "js1", email: "saxjax@saxjax.dk", balance: 100, lowBalanceDefinition: 50);
            User user2 = new User(id: 20, firstname: "jakob2", lastname: "skov", username: "js2", email: "saxjax@saxjax.dk", balance: 100, lowBalanceDefinition: 50);
            User user3 = new User(id: 3, firstname: "jakob3", lastname: "skov", username: "js3", email: "saxjax@saxjax.dk", balance: 100, lowBalanceDefinition: 50);
            User user4 = new User(id: 4, firstname: "jakob4", lastname: "skov", username: "js4", email: "saxjax@saxjax.dk", balance: 100, lowBalanceDefinition: 50);
            User user5 = new User(id: 50, firstname: "jakob5", lastname: "skov", username: "js5", email: "saxjax@saxjax.dk", balance: 100, lowBalanceDefinition: 50);
            User user6 = new User(id: 6, firstname: "jakob6", lastname: "skov", username: "js6", email: "saxjax@saxjax.dk", balance: 100, lowBalanceDefinition: 50);

            actualList.Add(user1);
            actualList.Add(user2);
            actualList.Add(user3);
            actualList.Add(user4);
            actualList.Add(user5);
            actualList.Add(user6);

            List<User> expectedList = new List<User>();

            expectedList.Add(user5);
            expectedList.Add(user2);
            expectedList.Add(user6);
            expectedList.Add(user4);
            expectedList.Add(user3);
            expectedList.Add(user1);





            //act
            actualList.Sort();

            //assert
            Assert.AreEqual(expectedList,actualList, $"{desc}, Sorted List: {actualList}");
        }


        ////Krav: Brugerens saldo. Man indbetaler penge på sin konto, og herefter kan man købe varer i systemet. Når brugerens saldo går under 50
        ////kroner skal brugeren advares, og ved yderligere køb bliver der vist en advarselsbesked. Introducér et delgate til denne slags hændelser
        //[TestCase("HandlerCalled", 50,50,1, "Test if Notification is triggered when balance of 50 with lowBalance set to 50 is withdrawed 1")]
        //[TestCase("HandlerCalled", -50, 50, 1, "Test if Notification is triggered when NEGATIVE balance of -50 with lowBalance set to 50 is withdrawed 1")]
        //[TestCase("HandlerCalled", -50, -50, 1, "Test if Notification is triggered when NEGATIVE balance of 50 with lowBalance set to -50 is withdrawed 1")]

        //[TestCase("HandlerNotCalled", 50, 50, 0, "Test if Notification is NOT triggered when  balance of 50 with lowBalance set to 50 is withdrawed 0")]
        //[TestCase("HandlerNotCalled", 50, 50, -1, "Test if Notification is NOT triggered when  balance of 50 with lowBalance set to 50 is withdrawed 0")]

        //public void Balance_lowBalance_ShouldTriggerUserBalanceNotificationHandler(string expected,decimal balance,decimal lowBalanceDefinition,decimal withdrawAmount, string desc)
        //{
        //    //arrange
        //    decimal actualBallance = -1000;
        //    string actual = "HandlerNotCalled";
        //    void LowBalanceReachedHandler(Object sender, LowBalanceEventArgs e)
        //    {
        //        actualBallance = e.User.Balance;
        //        actual = "HandlerCalled";
        //    }

        //    User user = new User(id: 1, firstname: "jakob", lastname: "skov", username: "js", email: "j@s.dk", balance: balance, lowBalanceDefinition: lowBalanceDefinition);
        //    user.LowBalanceEvent += LowBalanceReachedHandler;

        //    //act
        //    user.MakeTransaction(-withdrawAmount);

        //    //assert
        //    Assert.AreEqual(expected,actual, $"balance:{actualBallance},threshold:{lowBalanceDefinition},{desc}");
        //}

        




    }
}