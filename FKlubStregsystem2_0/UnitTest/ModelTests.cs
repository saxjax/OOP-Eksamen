using NUnit.Framework;

namespace ModelTests
{
    public class ModelTest
    {
        [SetUp]
        public void Setup()
        {
        }

        //        User Testet i UserTest
        //En bruger har følgende egenskaber:
        //OK ID
        //OK et unikt tal-id der identificerer brugeren.Jo lavere tallet er, jo før blev man indmeldt i 
        //F-klubben.Dette tal er unikt blandt brugere. • Firstname
        //OK Brugerens fornavn(e). Dette må aldrig være null. • Lastname
        //OK Brugerens efternavn. Dette må aldrig være null. • Username
        //OK Brugerens brugernavn. Dette bliver brugt når der bliver købt et produkt (sat en streg). Det er brugernavnet alene, der identificerer brugeren i stregsystemet! Brugernavnet må indeholde 0- 9, små bogstaver og underscore: [0-9], [a-z], og '_' 1
        //OK Email
        //OK Brugerens email-adresse.Dette skal altid være en gyldig email-adresse efter følgende krav.
        //OK En email adresse består af local-part @domain, og kravene til de to dele er:
        //OK local-part må bestå af a-z, A-Z, og tallene 0-9 samt tegnene punktum, underscore og bindestreg ('.', '_' og '-')
        //OK domain må indeholde a-z, A-Z, og tallene 0-9 samt punktum og bindestreg.domain må ikke starte eller slutte med bindestreg/punktum.domain skal indeholde mindst et punktum.
        //OK Et eksempel på en gyldig email-adresse er: eksempel @domain.dk
        //OK Et eksempel på en ugyldig email-adresse er: eksempel(2)@-mit_domain.dk
        //OK Balance
        //OK Brugerens saldo.Man indbetaler penge på sin konto, og herefter kan man købe varer i systemet. Når brugerens saldo går under 50 kroner skal brugeren advares, og ved yderligere køb bliver der vist en advarselsbesked.Introducér et delgate til denne slags hændelser
        //OK UserBalanceNotification(User user, decimal balance) -er lavet som LowBalanceEvent samt  en normalisedBalanceEvent når brugeren er tilbage på god økonomi
        //OK User skal have en tostring bestående af: ◦ Fornavn(e) Efternavn (Email)--jeg har tilføjet username for at kunne bruge det når programmet kører
        //OK Klassen skal også implementere en fornuftig Equals-metode samt GetHashCode.
        //OK Klassen skal implementere IComparable, og sorteres på ID.
        //OK• Klassen skal implementere en fornuftig konstruktør
        [Test]
        public void Test1()
        {
            Assert.AreEqual(true, true);
        }

        //        Product Testet i seperat fil
        //Et produkt har følgende egenskaber: • ID
        //◦ ID er et unikt tal-id for produkter.Dette bruges når man skal købe et produkt.Produktets ID må ikke være under 1. Dette ID bruges senere til at identificere produkter unikt, bl.a.ved køb.
        //• Name
        //◦ Beskrivelse af produkt.Et eksempel kunne være ”1⁄2L vand & kildevand excl.pant". Navn må
        //aldrig være null. • Price
        //◦ Prisen på et produkt. Denne vil ofte blive justeret. • Active
        //◦ Et flag der afgør om et produkt er aktivt eller ej.Der findes produkter der ikke sælges mere, men stadig findes i databasen (det kan være de ikke kan skaffes, eller af anden årsag ikke er i stregsystemet mere).
        //• CanBeBoughtOnCredit
        //◦ Et flag der afgør om et produkt kan købes, selvom brugeren ikke har penge nok på sin konto.
        //Der findes produkter, f.eks.fester, som folk kan finde på at købe i sidste øjeblik.For ikke at udelukke impulsive folk, med for få penge på sin konto, fra arrangementer, kan nogle produkter købes på kredit. Som udgangspunkt skal der altid være penge nok på ens konto, hvis man vil købe et produkt.
        //• ToString kan med fordel returnere produktets ID, Navn og Pris, som ses på billedet.
        //• Klassen skal have en fornuftig konstruktør
        [Test]
        public void Test2()
        {
            Assert.AreEqual(true, true);
        }

        //        SeasonalProduct
        //I denne nye version af stregsystemet vil vi introducere en ny type produkt, SeasonalProduct, for at reducere manuelt arbejde.Denne type produkter har følgende egenskaber:
        //• ID
        //◦ ID er et unikt tal-id for produkter.Dette bruges når man skal købe et produkt. Produkt-ID må
        //ikke være under 1 og skal selvfølgelig være unikt for hvert produkt. • Name
        //◦ Beskrivelse af produkt.Et eksempel kunne være ”1⁄2L vand & kildevand excl. pant". Navn må aldrig være null.
        //• Active
        //◦ Afgør om et produkt er aktivt eller ej, altså, om vi er inden for sæson eller ej, og om
        //produktet i det hele taget er aktivt i systemet.
        //• Price
        //◦ Prisen på et produkt. • CanBeBoughtOnCredit
        //◦ Et flag der afgør om et produkt kan købes, selvom brugeren ikke har penge nok på sin konto.
        //• SeasonStartDate
        //◦ Specificerertidspunktetsæsonenstarterpå.
        //• SeasonEndDate
        //◦ Specificerertidspunktetsæsonenslutterpå.
        public void Test3()
        {
            Assert.AreEqual(true, true);
        }

        //        Transaction testet i Buy transaction og insert transaction
        //Transaktioner beskriver køb og indsættelse af penge.Generelt kan man sige følgende om transaktioner:
        //• ID
        //◦ En transaktion har et unikt ID. Det er vigtigt at kunne skelne transaktioner fra hinanden.Dette
        //ID bestemmer rækkefølgen af transaktioner. • User
        //◦ Den bruger der er en del af transaktionen. Denne må ikke være null. • Date
        //◦ Dato for transaktion.Denne dato skal så vidt muligt være korrekt!
        //• Amount
        //◦ Et beløb der beskriver bevægelsen på brugerens konto.Negative beløb hæves og positive
        //beløb indsættes. • ToString
        //◦ Transaction har en specialiseret ToString metode der beskriver transaktionid, user, beløb og tidspunktet transaktionen blev foretaget.
        //• Execute
        //◦ Transaction specificerer at der findes en metode kaldet Execute.Denne metode udfører
        //transaktionen.
        //• En Transaction skal have en fornuftig konstruktør.Det vil bl.a.sige en konstruktør
        //parameteriseret med User.
        public void Test4()
        {
            Assert.AreEqual(true, true);
        }

        //        InsertCashTransaction
        //InsertCachTransaction bruges når der skal optankes penge på brugerens konto, og har følgende: • ToString
        //◦ Denne transactionstype har en specialiseret ToString, der fortæller at der er tale om en indbetaling, beløb, bruger, og hvornår indbetalingen blev foretaget og id på transaktionen. (du bestemmer rækkefølge og detaljer)
        //• Execute
        //◦ Lægger beløbet til brugerens konto.
        public void Test5()
        {
            Assert.AreEqual(true, true);
        }

        //        BuyTransaction
        //BuyTransaction bruges til at repræsentere køb, og er karakteriseret ved følgende • Product
        //◦ Det produkt der købes i stregsystemets • Amount
        //◦ Amount er prisen på produktet på købstidspunktet.Produkter ændrer pris over tid, men ikke med tilbagevirkende kraft!
        //• ToString
        //◦ Denne transactiontype har en specialiseret ToString der fortæller at der er tale om et køb,
        //beløb, bruger, produkt, hvornår købet blev foretaget, og transaktionens id. (du bestemmer
        //rækkefølge og detaljer) • Execute
        //◦ Trækker beløbet fra brugerens konto.Der opstår en fejlsituation hvis brugeren ikke har nok penge på sin konto og produktet, der købes, ikke tillader overtræk.Til dette formål defineres en exception, se nedenfor.
        //InsufficientCreditsException er en exception, der skal kastes, når en bruger forsøger, at købe et produkt der ikke er råd til.Denne exception indeholder information om brugeren og produktet, der forsøges købt, samt en herudfra beskrivende besked om fejlen.
        //Hvis der forsøges at købe et produkt der ikke er aktivt, skal der også kastes en passende exception.
        public void Test6()
        {
            Assert.AreEqual(true, true);
        }

        //        Stregsystem
        //Stregsystem indeholder ren logik vedrørende brugere, transaktioner og lignende.Det betyder at stregsystem indeholder informationer om brugere, produkter og transaktioner.
        //5
        //Stregsystemet har følgende skitserede metoder og properties: • BuyProduct(user, product)
        //◦ udfører den logik der køber et produkt på en brugers konto, ved brug af en transaktion • AddCreditsToAccount(user, amount)
        //◦ indsætter et beløb på en brugers konto, via en transaktion. • ExecuteTransaction(transaction)
        //◦ hjælpemetode til at eksekvere transaktioner, og sørge for at de bliver tilføjet til en liste af udførte transaktioner. Hvis transaktionen altså ikke fejler.
        //• GetProductByID(...)
        //◦ Finder og returnerer det unikke produkt i listen over produkter ud fra et produkt-id.Der bliver
        //kastet en brugerdefineret exception hvis produktet ikke eksisterer.Denne exception
        //indeholder information om produkt og beskrivende besked. • GetUsers(Func<User, bool> predicate)
        //◦ Brugere der overholder predicate
        //• GetUserByUsername(string username)
        //◦ Finder og returnerer den unikke bruger i brugerlisten ud fra brugernavn.Der bliver kastet en brugerdefineret exception hvis brugeren ikke findes. Denne exception indeholder information om bruger og beskrivende besked.
        //• GetTransactions(User user, int count)
        //◦ Finder et angivet (via parameter) antal transaktioner relateret til en given specifik bruger.
        //Det er de nyeste transaktioner der findes. • ActiveProducts
        //◦ aktive produkter i stregsystemet på nuværende tidspunkt
        //Derudover bør der være et event til at indikere at en bruger har faretruende få penge på sin konto(se User)
        public void Test7()
        {
            Assert.AreEqual(true, true);
        }

        //        Generelle krav til stregsystem
        //NOT OK  Logning: Alle udførte transaktioner skal skrives i en logfil.
        //OK Derudover bør der være funktionalitet nok til man kan undgå at få kastet exceptions.
        public void Test8()
        {
            Assert.AreEqual(true, false);
        }

        //        Varekatalog
        //Der er vedlagt en tekstfil med produkter.Der skal skrives en funktion til at indlæse tekstfilen,
        //products.csv, en semikolon separeret fil, med produkter. Første linie i filen indeholder beskrivelse af
        //værdier, resten er værdierne.Du kan med fordel ignorere deactivate_date.
        //Hvis en værdi indeholder mellemrum, er værdien indkapslet i quotes: ”værdi”.
        //HTML-tags bør fjernes.
        //Hver linie svarer til et produkt
        //Værdierne kommer altid i samme rækkefølge
        //Der vil altid være samme antal semikoloner på en linie!
        //Denne fil skal indlæses når stregsystemet startes, og aktive produkter skal vises i menuen.
        //Brugerdatabase: På samme måde som med varekataloget, er der vedlagt en tekstfil med brugere.
        public void Test9()
        {
            Assert.AreEqual(true, true);
        }


    }
}