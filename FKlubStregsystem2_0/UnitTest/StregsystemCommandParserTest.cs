using System;
using System.Collections.Generic;
using FKlubStregsystem;
using NUnit.Framework;

namespace StregsystemCommandParserTests
{
    public class StregsystemCommandParserTest
    {
        //Krav:Skal parse kommandoer fra tekst til funktionskald


        //krav: ParseCommand(string command)
        //krav UserCommands:
       

        //[TestCase("1","ugyldig bruger Skal  trigger et UserNotFoundException, og kalde DisplayUserNotFound")]//UserNotFoundException//FormatException//
        //   //done                                                                                                  //done

        //[TestCase("1 d d d d", "ugyldig command  trigger et ToomanuArguments, og kalde DisplayUserNotFound")]//UserNotFoundException//FormatException//
        ////done

        ////IRelevnt
        ////[TestCase("for langt brugernavn Skal  trigger et UserNotFoundException, og kalde DisplayUserNotFound")]//ArgumentOutOfRangeException//
        ////done

        ////krav:<username> <prodID> : køber et produkt
        //[TestCase("gyldig bruger og Produkt: Skal  oprette en transaktion og execute den derefter og kalde  UI.DisplayUserBuysProduct(tr)")]
        ////done
        //[TestCase("ugyldig bruger Skal  trigger en UserNotFoundException, og kalde DisplayUserNotFound")]
        ////done
        //[TestCase("for langt brugernavn Skal  trigger et UserNotFoundException, og kalde DisplayUserNotFound")]
        ////done

        //[TestCase("ugyldig Product id Skal  trigger en ProductNotFoundException, og kalde DisplayProductNotFound")]//FormatException// ArgumentOutOfRangeException// OutOfMemoryException
        //[TestCase("for langt Produkt id Skal  trigger et ProductNotFoundException, og kalde DisplayProductNotFound")]
        ////done

        //[TestCase("inaktivt Product id Skal  trigger en ProductNotActivatedException, og kalde DisplayProductNotFound")]
        ////done

        ////krav:<username> <prodID> <antal> : køber et antal af produktet
        //[TestCase("gyldig bruger og Produkt og antal: Skal  oprette en antal transaktioner og execute dem en ad gangen  og  kalde  UI.DisplayUserBuysProduct(tr) samme antal gange")]
        ////done
        //[TestCase("ugyldig bruger Skal  trigger en UserNotFoundException, og kalde DisplayUserNotFound")]
        ////done


        ////IRelevnt
        ////[TestCase("for langt brugernavn Skal  trigger et UserNotFoundException, og kalde DisplayUserNotFound")]
        ////done

        //[TestCase("ugyldig Product id Skal  trigger en ProductNotFoundException, og kalde DisplayProductNotFound")]//FormatException// ArgumentOutOfRangeException// OutOfMemoryException
        //[TestCase("for langt Produkt id Skal  trigger et ProductNotFoundException, og kalde DisplayProductNotFound")]
        ////done

        //[TestCase("inaktivt Product id Skal  trigger en ProductNotActivatedException, og kalde DisplayProductNotFound")]
        ////done

        //[TestCase("ugyldig antal format  Skal  trigger en FormatException, og kalde DisplayProductNotFound")]//FormatException// ArgumentOutOfRangeException// OutOfMemoryException
        ////done


        ////krav AdminCommands:
        ////krav: <:command> : er en admin kommando

        //[TestCase("ugyldig command Skal  trigger et KeyNotFoundException, og kalde UI.DisplayAdminCommandNotFoundMessage")] //KeyNotFoundException//MissingMethodException//UserNotFoundException//FormatException//
        ////done

        ////krav: :quit og :q  :skal afslutte programmet
        //[TestCase("gyldig command : Skal  afslutte programmet ved at kalde UI.Close")]
        ////done

        ////krav: :activate   (efterfulgt af produkt-id) :skal aktivere
        //[TestCase("gyldig command : Skal aktivere et produkt og kalde UI.DisplayProducts(new List<Product>() { p }")]
        ////done
        //[TestCase("ugyldig Product id Skal  trigger en ProductNotFoundException, og kalde DisplayProductNotFound")]//FormatException// ArgumentOutOfRangeException// OutOfMemoryException
        ////
        //[TestCase("ugyldig command Skal  trigger et KeyNotFoundException, og kalde UI.DisplayAdminCommandNotFoundMessage")] //KeyNotFoundException//MissingMethodException//UserNotFoundException//FormatException//
        ////done

        ////krav: :deactivate (efterfulgt af produkt-id) :skal deaktivere
        //[TestCase("gyldig command : Skal deaktivere et produkt og kalde UI.DisplayProducts(new List<Product>() { p }")]
        ////done

        ////krav: :crediton og :creditoff (efterfulgt af produkt-id)  :skal henholdsvis aktivere og deaktivere om et produkt kan købes på kredit
        //[TestCase("gyldig command : Skal sætte crediton for et produkt og kalde UI.DisplayProducts(new List<Product>() { p }")]
        ////done
        //[TestCase("ugyldig Product id Skal  trigger en ProductNotFoundException, og kalde DisplayProductNotFound")]//FormatException// ArgumentOutOfRangeException// OutOfMemoryException
        ////done
        //[TestCase("ugyldig command Skal  trigger et KeyNotFoundException, og kalde UI.DisplayAdminCommandNotFoundMessage")] //KeyNotFoundException//MissingMethodException//UserNotFoundException//FormatException//
        //                                                                                                                    //done



        //krav <username> : giver info om user
        [TestCase(":manual", "gyldig bruger : Skal returnere Brugerinfo og kalde DisplayUserInfo")]
        //done
        //krav: :insertmoney <username> <amount> :skal tilføje et antal credits til et brugernavns saldo
        [TestCase(":insertmoney js1 10", "gyldig command : Skal indsætte amount på brugernavnets balance og kalde UI.DisplayUserInfo(user)")]
        ////done
        public void CommandParser_Command_ShouldNotTriggerException(string command, string desc)
        {
            //arrange
            User user1 = new User(id: 1, firstname: "jakob1", lastname: "skov", username: "js1", email: "saxjax@saxjax.dk", balance: 100, lowBalanceDefinition: 50);
            User user2 = new User(id: 2, firstname: "jakob1", lastname: "skov", username: "js2", email: "saxjax@saxjax.dk", balance: 100, lowBalanceDefinition: 50);
            User user3 = new User(id: 3, firstname: "jakob1", lastname: "skov", username: "js3", email: "saxjax@saxjax.dk", balance: 100, lowBalanceDefinition: 50);
            Product A = new Product(id: 1, name: "A", price: 1);
            Product B = new Product(id: 10, name: "B", price: 10);
            Product C = new Product(id: 33, name: "C", price: 100);
            List<Product> products = new List<Product>() { A, B, C };
            List<User> users = new List<User>() { user1,user2,user3 };
            Stregsystem stregsystem = new Stregsystem(users: users, products: products);
            IStregsystemUI ui = new StregsystemCLI(stregsystem);
            StregsystemController sc = new StregsystemController(ui, stregsystem);
            
            //act
            //assert
            Assert.DoesNotThrow(() => ui.Start(command), $"{desc}: exception");
        }

        //Jeg kan visuelt konfirmere at det virker:



        //[TestCase(":insertmoney 11 10", "ugyldig bruger Skal  trigger en UserNotFoundException, og kalde DisplayUserNotFound")]
        //   //done

        //public void Insertmoney_userSetTo11_ShouldTriggerProductNotFoundException(string command, string desc)
        //{
        //    //arrange
        //    User user = new User(id: 1, firstname: "jakob1", lastname: "skov", username: "js1", email: "saxjax@saxjax.dk", balance: 100, lowBalanceDefinition: 50);
        //    Product A = new Product(id: 1, name: "A", price: 1);
        //    Product B = new Product(id: 10, name: "B", price: 10);
        //    Product C = new Product(id: 33, name: "C", price: 100);
        //    List<Product> products = new List<Product>() { A, B, C };
        //    List<User> users = new List<User>() { user };
        //    Stregsystem stregsystem = new Stregsystem(users: users, products: products);
        //    IStregsystemUI ui = new StregsystemCLI(stregsystem);
        //    StregsystemController sc = new StregsystemController(ui, stregsystem);
        //    //act
        //    //assert
        //    Assert.Throws<UserNotFoundException>(() => ui.Start($"{command}"), $"{desc}: exception");
        //}

        //[TestCase(":insertmoney js1 xxx","ugyldig amount format  Skal  trigger en ProductNotFoundException, og kalde DisplayProductNotFound")]//FormatException// ArgumentOutOfRangeException// OutOfMemoryException
        ////done
        //public void Insertmoney_amountSetToxxx_ShouldTriggerProductNotFoundException(string command,string desc)
        //{
        //    //arrange
        //    User user = new User(id: 1, firstname: "jakob1", lastname: "skov", username: "js1", email: "saxjax@saxjax.dk", balance: 100, lowBalanceDefinition: 50);
        //    Product A = new Product(id: 1, name: "A", price: 1);
        //    Product B = new Product(id: 10, name: "B", price: 10);
        //    Product C = new Product(id: 33, name: "C", price: 100);
        //    List<Product> products = new List<Product>() { A, B, C };
        //    List<User> users = new List<User>() { user };
        //    Stregsystem stregsystem = new Stregsystem(users: users, products: products);
        //    IStregsystemUI ui = new StregsystemCLI(stregsystem);
        //    StregsystemController sc = new StregsystemController(ui, stregsystem);

        //    //act
        //    //assert
        //    Assert.Throws<FormatException>(() => ui.Start($"{command}"),$"{desc}: exception");
        //}

        //krav: En kommando kan have parametre, de er separeret af mellemrum.
        // <usename> <prodID>
        //ParseCommand vil parse og bestemme at der er tale om et køb.


        //Der vil blive tjekket om brugernavn findes, samt om strengen ”<prodID>”
        //indeholder et tal, og om det er en gyldig reference til et produkt.


        //Er dette overholdt, vil der blive kaldt passende metoder på instansen
        //af Stregsystem, og ud fra resultatet, vil der blive kaldt en passende
        //metode på IStregsystemUI referencen.


        //I tilfælde af at købet går godt, vil metoden DisplayUserBuysProduct
        //blive kaldt, som vil opdatere brugergrænsefladen med passende tekst.

        //Brugergrænsefladen vil nu være klar til at modtage nye kommandoer.


        //Der skal triggeres et event og vises en besked når user.balance komer under et beløb, LowBalanceDefinition
        //done
        //Et andet event triggeres på samme måde når saldo går fra at være lav til at være normal
        //done



       





    }
}
