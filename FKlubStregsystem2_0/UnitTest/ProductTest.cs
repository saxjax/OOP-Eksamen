using System;
using System.Collections.Generic;
using FKlubStregsystem;
using NUnit.Framework;

namespace ProductTests
{
    public class ProductTest
    {
        //Krav:ID
        //ID er et unikt tal-id for produkter. Dette bruges når man skal
        //købe et produkt.
        //Produktets ID må ikke være under 1.
        //Dette ID bruges senere til at identificere produkter unikt, bl.a. ved køb.
        [TestCase("Exception Should be  thrown when ID is set to less than 1")]
        public void ID_SetTo0_ShouldThrowException(string desc)
        {
            //arrange
            Product p = new Product(id: 1, name: "a", price: 1, active: true, canBeBoughtOnCredit: false);

            //act

            //assert
            Assert.Throws<ArgumentException>(() => p.ID = 0, desc);
        }

        //krav:Name
        //Beskrivelse af produkt.Et eksempel kunne være ”1⁄2L vand & kildevand excl.pant".
        //Navn må aldrig være null.
        [TestCase("Exception Should be  thrown when ID is set to less than 1")]
        public void Name_SetToNull_ShouldThrowException(string desc)
        {
            //arrange
            Product p = new Product(id: 1, name: "a", price: 1, active: true, canBeBoughtOnCredit: false);

            //act

            //assert
            Assert.Throws<ArgumentNullException>(() => p.Name = null, desc);
        }

        //krav: Price
        //Prisen på et produkt. Denne vil ofte blive justeret.
        //Ingen Test


        //krav: Active
        //Et flag der afgør om et produkt er aktivt eller ej. Der findes
        //produkter der ikke sælges mere, men stadig findes i databasen
        //(det kan være de ikke kan skaffes, eller af anden årsag ikke er i
        //stregsystemet mere).
        //Ingen Test


        //krav: CanBeBoughtOnCredit
        //Et flag der afgør om et produkt kan købes, selvom brugeren ikke har
        //penge nok på sin konto.
        //Der findes produkter, f.eks.fester, som folk kan finde på at købe i
        //sidste øjeblik.For ikke at udelukke impulsive folk, med for få penge
        //på sin konto, fra arrangementer, kan nogle produkter købes på kredit.
        //Som udgangspunkt skal der altid være penge nok på ens konto, hvis man
        //vil købe et produkt.
        //Ingen Test


        //krav: ToString
        //ToString kan med fordel returnere produktets ID, Navn og Pris,
        //som ses på billedet.--Jeg har lavet det lidt mere fancy for at kunne stille produkter op i kolonner
        [TestCase("ToString should return ID, Name, Price")]
        public void ToString_Product_ShouldReturnStringIDNamePrice(string desc)
        {
            //arrange
            Product p = new Product(id: 1, name: "a", price: 22, active: true, canBeBoughtOnCredit: false);
            string expected = "id:1\t name:a                                        price:22            \t"; //"id:1\t name:a price:22";
            //act
            string actual = p.ToString();

            //assert
            Assert.AreEqual(expected, actual, desc);
        }



    }
}
