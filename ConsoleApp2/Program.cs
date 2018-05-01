using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {

        public class Card
        {
            public bool isLand = true;
            public bool tapped = false;
            public bool TapSansMontage = false;
            public bool TapSansIle = false;
            public bool TapSansMarais = false;
            public bool fastland = false;
            public bool montagne = false;
            public bool ile = false;
            public bool marais = false;
            public bool hub = false;
            public bool basic = false;
            public string nom;

            public bool IsRed()
            {
                if (this.montagne || this.TapSansMontage || this.hub || this.fastland)
                {
                    return true;
                }
                return false;
            }

            public bool IsBlack()
            {
                if (this.marais || this.TapSansMarais || this.hub)
                {
                    return true;
                }
                return false;
            }
            public bool IsBlue()
            {
                if (this.ile || this.TapSansIle || this.hub || this.fastland)
                {
                    return true;
                }
                return false;
            }

        }

        public class Etat
        {
            public int tour = 1;
            List<Card> deck = new List<Card>();
            List<Card> main = new List<Card>();
            public int nbTerrain = 0;
            public int nbBleu = 0;
            public int nbNoir = 0;
            public int nbRouge = 0;
            bool basicIlsandInPlay = false;
            bool basicMoutainInPlay = false;
            bool basicSwampInPlay = false;


            public void SetDeck(int nbcartes, int ile, int montagne, int marais, int aetherHub, int SommetCraneDragon, int CanyonCroupissant, int SpireBlufCanal, int ChuteSoufre, int CatacombesNoyees, int BassinFetides)
            {
                for (int j = 1; j <= ile; j++)
                {
                    Card card = new Card();
                    card.ile = true;
                    card.basic = true;
                    deck.Add(card);
                    card.nom = "ile";
                }
                for (int j = 1; j <= montagne; j++)
                {
                    Card card = new Card();
                    card.montagne = true;
                    card.basic = true;
                    deck.Add(card);
                    card.nom = "montagne";
                }
                for (int j = 1; j <= marais; j++)
                {
                    Card card = new Card();
                    card.marais = true;
                    card.basic = true;
                    deck.Add(card);
                    card.nom = "marais";
                }
                for (int j = 1; j <= aetherHub; j++)
                {
                    Card card = new Card();
                    card.hub = true;
                    deck.Add(card);
                    card.nom = "aetherHub";
                }
                for (int j = 1; j <= SommetCraneDragon; j++)
                {
                    Card card = new Card();
                    card.TapSansMontage = true;
                    card.TapSansMarais = true;
                    deck.Add(card);
                    card.nom = "SommetCraneDragon";
                }
                for (int j = 1; j <= CanyonCroupissant; j++)
                {
                    Card card = new Card();
                    card.marais = true;
                    card.montagne = true;
                    card.tapped = true;
                    deck.Add(card);
                    card.nom = "CanyonCroupissant";
                }
                for (int j = 1; j <= SpireBlufCanal; j++)
                {
                    Card card = new Card();
                    card.fastland = true;
                    deck.Add(card);
                    card.nom = "SpireBlufCanal";
                }
                for (int j = 1; j <= ChuteSoufre; j++)
                {
                    Card card = new Card();
                    card.TapSansMontage = true;
                    card.TapSansIle = true;
                    deck.Add(card);
                    card.nom = "marais";
                }
                for (int j = 1; j <= CatacombesNoyees; j++)
                {
                    Card card = new Card();
                    card.TapSansIle = true;
                    card.TapSansMarais = true;
                    deck.Add(card);
                    card.nom = "CatacombesNoyees";
                }
                for (int j = 1; j <= BassinFetides; j++)
                {
                    Card card = new Card();
                    card.marais = true;
                    card.ile = true;
                    card.tapped = true;
                    deck.Add(card);
                    card.nom = "BassinFetides";
                }

                // le reste est autre chose 
                for (int j = 1; deck.Count < nbcartes; j++)
                {
                    Card card = new Card();
                    card.isLand = false;
                    deck.Add(card);
                }

                Shuffle();

            }

            public void Shuffle()
            {
                int n = deck.Count;
                Random rnd = new Random();
                while (n > 1)
                {
                    int k = (rnd.Next(0, n) % n);
                    n--;
                    Card value = deck[k];
                    deck[k] = deck[n];
                    deck[n] = value;
                }
            }

            public Card ChoisirTerrain()
            {
                if (nbTerrain == 0)
                {
                    //jouer un cycle land rouge/noir
                    foreach (Card carte in main)
                    {
                        if (carte.tapped && carte.montagne)
                        {
                            return carte;
                        }
                    }

                    //jouer un cycle land bleu/noir
                    foreach (Card carte in main)
                    {
                        if (carte.tapped)
                        {
                            return carte;
                        }
                    }

                    //Sinon ile à moins que l'on ait le land rouge/noir sans une montagne
                    foreach (Card carte in main)
                    {
                        if (carte.basic && carte.ile)
                        {
                            foreach (Card carte2 in main)
                            {
                                if (carte2.basic && carte2.montagne)
                                {
                                    return carte;
                                }
                            }

                            foreach (Card carte2 in main)
                            {
                                if (carte2.TapSansMarais && carte2.TapSansMontage)
                                {
                                    return carte2;
                                }
                            }
                            return carte;
                        }
                    }

                    //On peut a le fastland et un tapland et le basicland qui permet de le jouer détappé on peut jouer le fastland
                    foreach (Card carte in main)
                    {
                        if (carte.fastland)
                        {
                            foreach (Card carte2 in main)
                            {
                                if (carte2.TapSansIle)
                                {
                                    foreach (Card carte3 in main)
                                    {
                                        if (carte3.basic && carte3.ile)
                                        {
                                            return carte;
                                        }
                                    }
                                }

                                if (carte2.TapSansMarais)
                                {
                                    foreach (Card carte3 in main)
                                    {
                                        if (carte3.basic && carte3.marais)
                                        {
                                            return carte;
                                        }
                                    }
                                }

                                if (carte2.TapSansMontage)
                                {
                                    foreach (Card carte3 in main)
                                    {
                                        if (carte3.basic && carte3.montagne)
                                        {
                                            return carte;
                                        }
                                    }
                                }
                            }
                        }
                    }

                    //Sinon les taplands par ordre d'intérêt
                    foreach (Card carte in main)
                    {
                        if (carte.TapSansIle && carte.TapSansMontage)
                        {
                            return carte;
                        }
                    }

                    foreach (Card carte in main)
                    {
                        if (carte.TapSansMarais && carte.TapSansMontage)
                        {
                            return carte;
                        }
                    }

                    foreach (Card carte in main)
                    {
                        if (carte.TapSansMarais && carte.TapSansIle)
                        {
                            return carte;
                        }
                    }

                    //Sinon on jour fastland
                    foreach (Card carte in main)
                    {
                        if (carte.fastland)
                        {
                            return carte;
                        }
                    }

                    //sinon montagne
                    foreach (Card carte in main)
                    {
                        if (carte.basic && carte.montagne)
                        {
                            return carte;
                        }
                    }

                    //sinon aether hub
                    foreach (Card carte in main)
                    {
                        if (carte.hub)
                        {
                            return carte;
                        }
                    }

                    //sinon marais
                    foreach (Card carte in main)
                    {
                        if (carte.basic && carte.montagne)
                        {
                            return carte;
                        }
                    }

                }

                if (nbTerrain == 1)
                {
                    //On joue fastland si possible
                    foreach (Card carte in main)
                    {
                        if (carte.fastland)
                        {
                            return carte;
                        }
                    }


                    //Lands qui nous ajoutent une couleur rouge et qui arrivent dégagés
                    if (nbRouge == 0)
                    {
                        if (basicIlsandInPlay || basicMoutainInPlay)
                        {
                            foreach (Card carte in main)
                            {
                                if (carte.TapSansIle && carte.TapSansMontage)
                                {
                                    return carte;
                                }
                            }
                        }

                        if (basicSwampInPlay || basicMoutainInPlay)
                        {
                            foreach (Card carte in main)
                            {
                                if (carte.TapSansMarais && carte.TapSansMontage)
                                {
                                    return carte;
                                }
                            }
                        }

                        foreach (Card carte in main)
                        {
                            if (carte.hub)
                            {
                                return carte;
                            }
                        }

                        foreach (Card carte in main)
                        {
                            if (carte.basic && carte.montagne)
                            {
                                return carte;
                            }
                        }
                    }
                    //Si on a un déjà rouge, land qui arrive dégagé
                    else
                    {
                        //Lands qui nous ajoutent le bleu et qui arrivent dégagés
                        if (nbBleu == 0)
                        {

                            if (basicIlsandInPlay || basicSwampInPlay)
                            {
                                foreach (Card carte in main)
                                {
                                    if (carte.TapSansIle && carte.TapSansMarais)
                                    {
                                        return carte;
                                    }
                                }
                            }

                            if (basicIlsandInPlay || basicMoutainInPlay)
                            {
                                foreach (Card carte in main)
                                {
                                    if (carte.TapSansIle && carte.TapSansMontage)
                                    {
                                        return carte;
                                    }
                                }
                            }

                            if (basicMoutainInPlay || basicSwampInPlay)
                            {
                                foreach (Card carte in main)
                                {
                                    if (carte.TapSansMontage && carte.TapSansMarais)
                                    {
                                        return carte;
                                    }
                                }
                            }

                            //ile
                            foreach (Card carte in main)
                            {
                                if (carte.basic && carte.ile)
                                {
                                    return carte;
                                }
                            }

                            //marais
                            foreach (Card carte in main)
                            {
                                if (carte.basic && carte.marais)
                                {
                                    return carte;
                                }
                            }

                            //hub
                            foreach (Card carte in main)
                            {
                                if (carte.hub)
                                {
                                    return carte;
                                }
                            }

                            //montagne
                            foreach (Card carte in main)
                            {
                                if (carte.basic && carte.montagne)
                                {
                                    return carte;
                                }
                            }
                        }
                        //si on a un bleu et un rouge
                        else
                        {

                            //Acun bland ne peut arriver untap tour 2

                            //marais
                            foreach (Card carte in main)
                            {
                                if (carte.basic && carte.marais)
                                {
                                    return carte;
                                }
                            }

                            //ile
                            foreach (Card carte in main)
                            {
                                if (carte.basic && carte.ile)
                                {
                                    return carte;
                                }
                            }

                            //montagne
                            foreach (Card carte in main)
                            {
                                if (carte.basic && carte.montagne)
                                {
                                    return carte;
                                }
                            }

                            //hub
                            foreach (Card carte in main)
                            {
                                if (carte.hub)
                                {
                                    return carte;
                                }
                            }
                        }
                    }

                    //Si on a pas moyen d'avoi deux terrains dégagés dont au moins un rouge, autant privilégier les lands qui arrivent engagés

                    //jouer un cycle land bleu/noir sauf si on a déjà du bleu et qu'on pourrait avoir du rouge
                    foreach (Card carte in main)
                    {
                        if (carte.tapped && carte.ile)
                        {
                            //on préfère jouer un cycle land rouge/noir si on a déjà du bleu
                            if (nbBleu > 0)
                            {
                                foreach (Card carte2 in main)
                                {
                                    if (carte2.tapped && carte2.montagne)
                                    {
                                        return carte2;
                                    }
                                }
                            }
                            return carte;
                        }
                    }

                    //jouer un cycle land rouge/noir
                    foreach (Card carte in main)
                    {
                        if (carte.tapped && carte.montagne)
                        {
                            return carte;
                        }
                    }
                    //tapland bleu noir si déjà du rouge
                    if (nbRouge > 0)
                    {
                        foreach (Card carte in main)
                        {
                            if (carte.TapSansIle && carte.TapSansMarais)
                            {
                                return carte;
                            }
                        }
                    }
                    //tapland rouge noir si déjà du bleu
                    if (nbBleu > 0)
                    {
                        foreach (Card carte in main)
                        {
                            if (carte.TapSansMontage && carte.TapSansMarais)
                            {
                                return carte;
                            }
                        }
                    }
                    //tapland bleu rouge
                    foreach (Card carte in main)
                    {
                        if (carte.TapSansIle && carte.TapSansMontage)
                        {
                            return carte;
                        }
                    }

                    //tapland bleu noir
                    foreach (Card carte in main)
                    {
                        if (carte.TapSansIle && carte.TapSansMarais)
                        {
                            return carte;
                        }
                    }

                    //tapland rouge noir
                    foreach (Card carte in main)
                    {
                        if (carte.TapSansMontage && carte.TapSansMarais)
                        {
                            return carte;
                        }
                    }



                    //montagne si deja ile ou marais déjà gérée dans un cas précédent

                    //marais si ile
                    if (basicIlsandInPlay)
                    {
                        foreach (Card carte in main)
                        {
                            if (carte.basic && carte.marais)
                            {
                                return carte;
                            }
                        }
                    }
                    //ile
                    foreach (Card carte in main)
                    {
                        if (carte.basic && carte.ile)
                        {
                            return carte;
                        }
                    }

                    //marais
                    foreach (Card carte in main)
                    {
                        if (carte.basic && carte.marais)
                        {
                            return carte;
                        }
                    }

                    //aether hub
                    foreach (Card carte in main)
                    {
                        if (carte.hub)
                        {
                            return carte;
                        }
                    }
                }

                if (nbTerrain == 2)
                {
                    //On joue fastland si possible
                    foreach (Card carte in main)
                    {
                        if (carte.fastland)
                        {
                            return carte;
                        }
                    }


                    //Lands qui nous ajoutent une couleur bleue et qui arrivent dégagés
                    if (nbBleu == 0)
                    {
                        //Si c'est possible d'ajouter le noir parce qu'on a déjà rouge c'est cool
                        if (nbRouge > 0 && (basicSwampInPlay || basicMoutainInPlay))
                        {
                            foreach (Card carte in main)
                            {
                                if (carte.TapSansMarais && carte.TapSansMontage)
                                {
                                    return carte;
                                }
                            }
                        }

                        if (basicIlsandInPlay || basicMoutainInPlay)
                        {
                            foreach (Card carte in main)
                            {
                                if (carte.TapSansIle && carte.TapSansMontage)
                                {
                                    return carte;
                                }
                            }
                        }

                        if (basicSwampInPlay || basicIlsandInPlay)
                        {
                            foreach (Card carte in main)
                            {
                                if (carte.TapSansMarais && carte.TapSansIle)
                                {
                                    return carte;
                                }
                            }
                        }

                        foreach (Card carte in main)
                        {
                            if (carte.hub)
                            {
                                return carte;
                            }
                        }

                        foreach (Card carte in main)
                        {
                            if (carte.basic && carte.ile)
                            {
                                return carte;
                            }
                        }
                    }
                    //Si on a un déjà bleu, land qui arrive dégagé
                    else
                    {
                        //Si on a pas encore de rouge, il serait temps d'en avoir
                        if (nbRouge == 0)
                        {
                            //Si en plus on peut ajouter le noir c'est la fête
                            if (basicMoutainInPlay || basicSwampInPlay)
                            {
                                foreach (Card carte in main)
                                {
                                    if (carte.TapSansMontage && carte.TapSansMarais)
                                    {
                                        return carte;
                                    }
                                }
                            }

                            //Du rouge et du bleu, c'est pas mal aussi pour double bleu sauf si on a déjà double bleu
                            if (nbBleu > 1 && (basicIlsandInPlay || basicMoutainInPlay))
                            {
                                foreach (Card carte in main)
                                {
                                    if (carte.TapSansIle && carte.TapSansMontage)
                                    {
                                        return carte;
                                    }
                                }
                            }

                            //montagne
                            foreach (Card carte in main)
                            {
                                if (carte.basic && carte.ile)
                                {
                                    return carte;
                                }
                            }

                            //tapland bleu/rouge
                            if (basicIlsandInPlay || basicMoutainInPlay)
                            {
                                foreach (Card carte in main)
                                {
                                    if (carte.TapSansIle && carte.TapSansMontage)
                                    {
                                        return carte;
                                    }
                                }
                            }

                            //hub
                            foreach (Card carte in main)
                            {
                                if (carte.hub)
                                {
                                    return carte;
                                }
                            }
                        }
                        //si on a un bleu et un rouge, on essaye d'avoir un noir
                        else
                        {
                            //tapland noir/bleu
                            if (basicIlsandInPlay || basicSwampInPlay)
                            {
                                foreach (Card carte in main)
                                {
                                    if (carte.TapSansIle && carte.TapSansMarais)
                                    {
                                        return carte;
                                    }
                                }
                            }

                            //tapland noir/rouge
                            if (basicMoutainInPlay || basicSwampInPlay)
                            {
                                foreach (Card carte in main)
                                {
                                    if (carte.TapSansMontage && carte.TapSansMarais)
                                    {
                                        return carte;
                                    }
                                }
                            }

                            //marais
                            foreach (Card carte in main)
                            {
                                if (carte.basic && carte.marais)
                                {
                                    return carte;
                                }
                            }

                            //hub
                            foreach (Card carte in main)
                            {
                                if (carte.hub)
                                {
                                    return carte;
                                }
                            }

                            //tapland bleu/rouge
                            if (basicIlsandInPlay || basicMoutainInPlay)
                            {
                                foreach (Card carte in main)
                                {
                                    if (carte.TapSansIle && carte.TapSansMontage)
                                    {
                                        return carte;
                                    }
                                }
                            }

                            //ile
                            foreach (Card carte in main)
                            {
                                if (carte.basic && carte.ile)
                                {
                                    return carte;
                                }
                            }

                            //montagne
                            foreach (Card carte in main)
                            {
                                if (carte.basic && carte.montagne)
                                {
                                    return carte;
                                }
                            }

                        }
                    }

                    //Si on a pas moyen d'avoir trois terrains dégagés dont au moins un bleu, autant privilégier les lands qui arrivent engagés

                    //jouer un cycle land bleu/noir sauf si on a déjà du bleu et qu'on pourrait avoir du rouge
                    foreach (Card carte in main)
                    {
                        if (carte.tapped && carte.ile)
                        {
                            //on préfère jouer un cycle land rouge/noir si on a déjà du bleu
                            if (nbBleu > 0)
                            {
                                foreach (Card carte2 in main)
                                {
                                    if (carte2.tapped && carte2.montagne)
                                    {
                                        return carte2;
                                    }
                                }
                            }
                            return carte;
                        }
                    }

                    //jouer un cycle land rouge/noir
                    foreach (Card carte in main)
                    {
                        if (carte.tapped && carte.montagne)
                        {
                            return carte;
                        }
                    }
                    //tapland bleu noir si déjà du rouge
                    if (nbRouge > 0)
                    {
                        foreach (Card carte in main)
                        {
                            if (carte.TapSansIle && carte.TapSansMarais)
                            {
                                return carte;
                            }
                        }
                    }
                    //tapland rouge noir si déjà du bleu
                    if (nbBleu > 0)
                    {
                        foreach (Card carte in main)
                        {
                            if (carte.TapSansMontage && carte.TapSansMarais)
                            {
                                return carte;
                            }
                        }
                    }
                    //tapland bleu rouge
                    foreach (Card carte in main)
                    {
                        if (carte.TapSansIle && carte.TapSansMontage)
                        {
                            return carte;
                        }
                    }

                    //tapland bleu noir
                    foreach (Card carte in main)
                    {
                        if (carte.TapSansIle && carte.TapSansMarais)
                        {
                            return carte;
                        }
                    }

                    //tapland rouge noir
                    foreach (Card carte in main)
                    {
                        if (carte.TapSansMontage && carte.TapSansMarais)
                        {
                            return carte;
                        }
                    }

                    //marais si ile ou montagne
                    if (basicIlsandInPlay || basicMoutainInPlay)
                    {
                        foreach (Card carte in main)
                        {
                            if (carte.basic && carte.marais)
                            {
                                return carte;
                            }
                        }
                    }

                    //ile si montagne ou marais
                    if (basicSwampInPlay || basicMoutainInPlay)
                    {
                        foreach (Card carte in main)
                        {
                            if (carte.basic && carte.ile)
                            {
                                return carte;
                            }
                        }
                    }

                    //montagne si ile ou marais
                    if (basicSwampInPlay || basicIlsandInPlay)
                    {
                        foreach (Card carte in main)
                        {
                            if (carte.basic && carte.montagne)
                            {
                                return carte;
                            }
                        }
                    }

                    //marais
                    foreach (Card carte in main)
                    {
                        if (carte.basic && carte.marais)
                        {
                            return carte;
                        }
                    }

                    //ile
                    foreach (Card carte in main)
                    {
                        if (carte.basic && carte.ile)
                        {
                            return carte;
                        }
                    }

                    //montagne
                    foreach (Card carte in main)
                    {
                        if (carte.basic && carte.montagne)
                        {
                            return carte;
                        }
                    }

                    //aether hub
                    foreach (Card carte in main)
                    {
                        if (carte.hub)
                        {
                            return carte;
                        }
                    }
                }

                if (nbTerrain == 3)
                {

                    //Lands qui nous ajoutent une couleur bleue et qui arrivent dégagés
                    if (nbBleu == 0)
                    {
                        //Si c'est possible d'ajouter le noir parce qu'on a déjà rouge c'est cool
                        if (nbRouge > 0 && (basicSwampInPlay || basicMoutainInPlay))
                        {
                            foreach (Card carte in main)
                            {
                                if (carte.TapSansMarais && carte.TapSansMontage)
                                {
                                    return carte;
                                }
                            }
                        }

                        if (basicIlsandInPlay || basicMoutainInPlay)
                        {
                            foreach (Card carte in main)
                            {
                                if (carte.TapSansIle && carte.TapSansMontage)
                                {
                                    return carte;
                                }
                            }
                        }

                        if (basicSwampInPlay || basicIlsandInPlay)
                        {
                            foreach (Card carte in main)
                            {
                                if (carte.TapSansMarais && carte.TapSansIle)
                                {
                                    return carte;
                                }
                            }
                        }

                        foreach (Card carte in main)
                        {
                            if (carte.hub)
                            {
                                return carte;
                            }
                        }

                        foreach (Card carte in main)
                        {
                            if (carte.basic && carte.ile)
                            {
                                return carte;
                            }
                        }

                        //Sinon si on peux avoir deux noirs on essaye d'avoir 4 lands untap
                        if (nbNoir == 1)
                        {
                            //On joue un land noir qui arrive untap

                            //tapland noir/bleu : on l'aurait joué au dessus si ça avait été possible

                            //tapland noir/rouge
                            if (basicMoutainInPlay || basicSwampInPlay)
                            {
                                foreach (Card carte in main)
                                {
                                    if (carte.TapSansMontage && carte.TapSansMarais)
                                    {
                                        return carte;
                                    }
                                }
                            }

                            //marais
                            foreach (Card carte in main)
                            {
                                if (carte.basic && carte.marais)
                                {
                                    return carte;
                                }
                            }

                            //hub
                            foreach (Card carte in main)
                            {
                                if (carte.hub)
                                {
                                    return carte;
                                }
                            }

                        }
                        else if (nbNoir > 1)
                        {
                            //tapland noir/rouge
                            if (basicMoutainInPlay || basicSwampInPlay)
                            {
                                foreach (Card carte in main)
                                {
                                    if (carte.TapSansMontage && carte.TapSansMarais)
                                    {
                                        return carte;
                                    }
                                }
                            }

                            //montagne
                            foreach (Card carte in main)
                            {
                                if (carte.basic && carte.montagne)
                                {
                                    return carte;
                                }
                            }

                            //hub
                            foreach (Card carte in main)
                            {
                                if (carte.hub)
                                {
                                    return carte;
                                }
                            }

                            //marais
                            foreach (Card carte in main)
                            {
                                if (carte.basic && carte.marais)
                                {
                                    return carte;
                                }
                            }
                        }
                    }
                    //Si on a un déjà bleu, land qui arrive dégagé
                    else
                    {
                        //Si on a pas encore de rouge, il serait temps d'en avoir
                        if (nbRouge == 0)
                        {
                            //Si en plus on peut ajouter le noir c'est la fête
                            if (basicMoutainInPlay || basicSwampInPlay)
                            {
                                foreach (Card carte in main)
                                {
                                    if (carte.TapSansMontage && carte.TapSansMarais)
                                    {
                                        return carte;
                                    }
                                }
                            }

                            //Du rouge et du bleu, c'est pas mal aussi pour double bleu sauf si on a déjà double bleu
                            if (nbBleu > 1 && (basicIlsandInPlay || basicMoutainInPlay))
                            {
                                foreach (Card carte in main)
                                {
                                    if (carte.TapSansIle && carte.TapSansMontage)
                                    {
                                        return carte;
                                    }
                                }
                            }

                            //montagne
                            foreach (Card carte in main)
                            {
                                if (carte.basic && carte.ile)
                                {
                                    return carte;
                                }
                            }

                            //tapland bleu/rouge
                            if (basicIlsandInPlay || basicMoutainInPlay)
                            {
                                foreach (Card carte in main)
                                {
                                    if (carte.TapSansIle && carte.TapSansMontage)
                                    {
                                        return carte;
                                    }
                                }
                            }

                            //hub
                            foreach (Card carte in main)
                            {
                                if (carte.hub)
                                {
                                    return carte;
                                }
                            }
                        }
                        //si on a un bleu et un rouge, on essaye d'avoir un noir
                        else
                        {
                            //tapland noir/bleu
                            if (basicIlsandInPlay || basicSwampInPlay)
                            {
                                foreach (Card carte in main)
                                {
                                    if (carte.TapSansIle && carte.TapSansMarais)
                                    {
                                        return carte;
                                    }
                                }
                            }

                            //tapland noir/rouge
                            if (basicMoutainInPlay || basicSwampInPlay)
                            {
                                foreach (Card carte in main)
                                {
                                    if (carte.TapSansMontage && carte.TapSansMarais)
                                    {
                                        return carte;
                                    }
                                }
                            }

                            //marais
                            foreach (Card carte in main)
                            {
                                if (carte.basic && carte.marais)
                                {
                                    return carte;
                                }
                            }

                            //hub
                            foreach (Card carte in main)
                            {
                                if (carte.hub)
                                {
                                    return carte;
                                }
                            }

                            //tapland bleu/rouge
                            if (basicIlsandInPlay || basicMoutainInPlay)
                            {
                                foreach (Card carte in main)
                                {
                                    if (carte.TapSansIle && carte.TapSansMontage)
                                    {
                                        return carte;
                                    }
                                }
                            }

                            //ile
                            foreach (Card carte in main)
                            {
                                if (carte.basic && carte.ile)
                                {
                                    return carte;
                                }
                            }

                            //montagne
                            foreach (Card carte in main)
                            {
                                if (carte.basic && carte.montagne)
                                {
                                    return carte;
                                }
                            }

                        }
                    }

                    //Si on a pas moyen d'avoir quatre terrains dégagés, autant privilégier les lands qui arrivent engagés

                    //jouer un cycle land bleu/noir sauf si on a déjà du bleu et qu'on pourrait avoir du rouge
                    foreach (Card carte in main)
                    {
                        if (carte.tapped && carte.ile)
                        {
                            //on préfère jouer un cycle land rouge/noir si on a que un seul bleu
                            if (nbBleu > 1)
                            {
                                foreach (Card carte2 in main)
                                {
                                    if (carte2.tapped && carte2.montagne)
                                    {
                                        return carte2;
                                    }
                                }
                            }
                            return carte;
                        }
                    }

                    //jouer un cycle land rouge/noir
                    foreach (Card carte in main)
                    {
                        if (carte.tapped && carte.montagne)
                        {
                            return carte;
                        }
                    }

                    //tapland bleu noir si déjà du rouge
                    if (nbRouge > 0)
                    {
                        foreach (Card carte in main)
                        {
                            if (carte.TapSansIle && carte.TapSansMarais)
                            {
                                return carte;
                            }
                        }
                    }
                    //tapland rouge noir si déjà du bleu
                    if (nbBleu > 0)
                    {
                        foreach (Card carte in main)
                        {
                            if (carte.TapSansMontage && carte.TapSansMarais)
                            {
                                return carte;
                            }
                        }
                    }

                    //tapland bleu noir si il arrive tappé
                    if (!basicSwampInPlay && !basicIlsandInPlay)
                    {
                        foreach (Card carte in main)
                        {
                            if (carte.TapSansIle && carte.TapSansMarais)
                            {
                                return carte;
                            }
                        }
                    }
                    //tapland rouge noir si il arrive tappé
                    if (!basicSwampInPlay && !basicMoutainInPlay)
                    {
                        foreach (Card carte in main)
                        {
                            if (carte.TapSansMontage && carte.TapSansMarais)
                            {
                                return carte;
                            }
                        }
                    }

                    //tapland bleu noir
                    foreach (Card carte in main)
                    {
                        if (carte.TapSansIle && carte.TapSansMarais)
                        {
                            return carte;
                        }
                    }

                    //tapland bleu rouge si il nous manque 1 bleu
                    if (nbBleu < 2)
                    {
                        //fastland
                        foreach (Card carte in main)
                        {
                            if (carte.fastland)
                            {
                                return carte;
                            }
                        }

                        foreach (Card carte in main)
                        {
                            if (carte.TapSansIle && carte.TapSansMontage)
                            {
                                return carte;
                            }
                        }
                    }

                    //tapland rouge noir
                    foreach (Card carte in main)
                    {
                        if (carte.TapSansMontage && carte.TapSansMarais)
                        {
                            return carte;
                        }
                    }

                    //fastland
                    foreach (Card carte in main)
                    {
                        if (carte.fastland)
                        {
                            return carte;
                        }
                    }
                    //tapland bleu rouge
                    foreach (Card carte in main)
                    {
                        if (carte.TapSansIle && carte.TapSansMontage)
                        {
                            return carte;
                        }
                    }

                    //marais si ile ou montagne
                    if (basicIlsandInPlay || basicMoutainInPlay)
                    {
                        foreach (Card carte in main)
                        {
                            if (carte.basic && carte.marais)
                            {
                                return carte;
                            }
                        }
                    }

                    //ile si montagne ou marais
                    if (basicSwampInPlay || basicMoutainInPlay)
                    {
                        foreach (Card carte in main)
                        {
                            if (carte.basic && carte.ile)
                            {
                                return carte;
                            }
                        }
                    }

                    //montagne si ile ou marais
                    if (basicSwampInPlay || basicIlsandInPlay)
                    {
                        foreach (Card carte in main)
                        {
                            if (carte.basic && carte.montagne)
                            {
                                return carte;
                            }
                        }
                    }

                    //marais
                    foreach (Card carte in main)
                    {
                        if (carte.basic && carte.marais)
                        {
                            return carte;
                        }
                    }

                    //ile
                    foreach (Card carte in main)
                    {
                        if (carte.basic && carte.ile)
                        {
                            return carte;
                        }
                    }

                    //montagne
                    foreach (Card carte in main)
                    {
                        if (carte.basic && carte.montagne)
                        {
                            return carte;
                        }
                    }

                    //aether hub
                    foreach (Card carte in main)
                    {
                        if (carte.hub)
                        {
                            return carte;
                        }
                    }
                }

                if (nbTerrain == 4)
                {
                    if (nbBleu == 0)
                    {
                        //on vise les 2 manas noirs
                        if (nbNoir == 1)
                        {
                            //On joue un land noir qui arrive untap

                            //tapland noir/bleu : on l'aurait joué au dessus si ça avait été possible

                            //tapland noir/rouge
                            if (basicMoutainInPlay || basicSwampInPlay)
                            {
                                foreach (Card carte in main)
                                {
                                    if (carte.TapSansMontage && carte.TapSansMarais)
                                    {
                                        return carte;
                                    }
                                }
                            }

                            //marais
                            foreach (Card carte in main)
                            {
                                if (carte.basic && carte.marais)
                                {
                                    return carte;
                                }
                            }

                            //hub
                            foreach (Card carte in main)
                            {
                                if (carte.hub)
                                {
                                    return carte;
                                }
                            }
                        }
                    }
                    //Lands qui nous ajoutent une couleur bleue et qui arrivent dégagés
                    else if (nbBleu == 1)
                    {
                        //Si c'est possible d'ajouter le noir parce qu'on a déjà rouge c'est cool
                        if (nbRouge > 0 && (basicSwampInPlay || basicMoutainInPlay))
                        {
                            foreach (Card carte in main)
                            {
                                if (carte.TapSansMarais && carte.TapSansMontage)
                                {
                                    return carte;
                                }
                            }
                        }

                        if (basicIlsandInPlay || basicMoutainInPlay)
                        {
                            foreach (Card carte in main)
                            {
                                if (carte.TapSansIle && carte.TapSansMontage)
                                {
                                    return carte;
                                }
                            }
                        }

                        if (basicSwampInPlay || basicIlsandInPlay)
                        {
                            foreach (Card carte in main)
                            {
                                if (carte.TapSansMarais && carte.TapSansIle)
                                {
                                    return carte;
                                }
                            }
                        }

                        foreach (Card carte in main)
                        {
                            if (carte.hub)
                            {
                                return carte;
                            }
                        }

                        foreach (Card carte in main)
                        {
                            if (carte.basic && carte.ile)
                            {
                                return carte;
                            }
                        }

                        //Sinon si on peux avoir deux noirs untap on y va
                        if (nbNoir == 1)
                        {
                            //On joue un land noir qui arrive untap

                            //tapland noir/bleu : on l'aurait joué au dessus si ça avait été possible

                            //tapland noir/rouge
                            if (basicMoutainInPlay || basicSwampInPlay)
                            {
                                foreach (Card carte in main)
                                {
                                    if (carte.TapSansMontage && carte.TapSansMarais)
                                    {
                                        return carte;
                                    }
                                }
                            }

                            //marais
                            foreach (Card carte in main)
                            {
                                if (carte.basic && carte.marais)
                                {
                                    return carte;
                                }
                            }

                            //hub
                            foreach (Card carte in main)
                            {
                                if (carte.hub)
                                {
                                    return carte;
                                }
                            }
                        }
                    }
                    //Si on a un déjà deux bleu, land qui arrive dégagé
                    else
                    {
                        //Si on a pas encore de rouge, il serait temps d'en avoir
                        if (nbRouge == 0)
                        {
                            //Si en plus on peut ajouter le noir c'est la fête
                            if (basicMoutainInPlay || basicSwampInPlay)
                            {
                                foreach (Card carte in main)
                                {
                                    if (carte.TapSansMontage && carte.TapSansMarais)
                                    {
                                        return carte;
                                    }
                                }
                            }

                            //Du rouge et du bleu, c'est pas mal aussi pour double bleu sauf si on a déjà double bleu
                            if (nbBleu > 1 && (basicIlsandInPlay || basicMoutainInPlay))
                            {
                                foreach (Card carte in main)
                                {
                                    if (carte.TapSansIle && carte.TapSansMontage)
                                    {
                                        return carte;
                                    }
                                }
                            }

                            //montagne
                            foreach (Card carte in main)
                            {
                                if (carte.basic && carte.ile)
                                {
                                    return carte;
                                }
                            }

                            //tapland bleu/rouge
                            if (basicIlsandInPlay || basicMoutainInPlay)
                            {
                                foreach (Card carte in main)
                                {
                                    if (carte.TapSansIle && carte.TapSansMontage)
                                    {
                                        return carte;
                                    }
                                }
                            }

                            //hub
                            foreach (Card carte in main)
                            {
                                if (carte.hub)
                                {
                                    return carte;
                                }
                            }
                        }
                        //si on a un bleu et un rouge, on essaye d'avoir un noir
                        else
                        {
                            //tapland noir/bleu
                            if (basicIlsandInPlay || basicSwampInPlay)
                            {
                                foreach (Card carte in main)
                                {
                                    if (carte.TapSansIle && carte.TapSansMarais)
                                    {
                                        return carte;
                                    }
                                }
                            }

                            //tapland noir/rouge
                            if (basicMoutainInPlay || basicSwampInPlay)
                            {
                                foreach (Card carte in main)
                                {
                                    if (carte.TapSansMontage && carte.TapSansMarais)
                                    {
                                        return carte;
                                    }
                                }
                            }

                            //marais
                            foreach (Card carte in main)
                            {
                                if (carte.basic && carte.marais)
                                {
                                    return carte;
                                }
                            }

                            //hub
                            foreach (Card carte in main)
                            {
                                if (carte.hub)
                                {
                                    return carte;
                                }
                            }

                            //tapland bleu/rouge
                            if (basicIlsandInPlay || basicMoutainInPlay)
                            {
                                foreach (Card carte in main)
                                {
                                    if (carte.TapSansIle && carte.TapSansMontage)
                                    {
                                        return carte;
                                    }
                                }
                            }

                            //ile
                            foreach (Card carte in main)
                            {
                                if (carte.basic && carte.ile)
                                {
                                    return carte;
                                }
                            }

                            //montagne
                            foreach (Card carte in main)
                            {
                                if (carte.basic && carte.montagne)
                                {
                                    return carte;
                                }
                            }

                        }
                    }

                    //Si on a pas moyen d'avoir quatre terrains dégagés, autant privilégier les lands qui arrivent engagés

                    //jouer un cycle land bleu/noir sauf si on a déjà du bleu et qu'on pourrait avoir du rouge
                    foreach (Card carte in main)
                    {
                        if (carte.tapped && carte.ile)
                        {
                            //on préfère jouer un cycle land rouge/noir si on a déjà du bleu
                            if (nbBleu > 0)
                            {
                                foreach (Card carte2 in main)
                                {
                                    if (carte2.tapped && carte2.montagne)
                                    {
                                        return carte2;
                                    }
                                }
                            }
                            return carte;
                        }
                    }

                    //jouer un cycle land rouge/noir
                    foreach (Card carte in main)
                    {
                        if (carte.tapped && carte.montagne)
                        {
                            return carte;
                        }
                    }

                    //tapland bleu noir si déjà du rouge
                    if (nbRouge > 0)
                    {
                        foreach (Card carte in main)
                        {
                            if (carte.TapSansIle && carte.TapSansMarais)
                            {
                                return carte;
                            }
                        }
                    }
                    //tapland rouge noir si déjà du bleu
                    if (nbBleu > 0)
                    {
                        foreach (Card carte in main)
                        {
                            if (carte.TapSansMontage && carte.TapSansMarais)
                            {
                                return carte;
                            }
                        }
                    }

                    //tapland bleu noir si il arrive tappé
                    if (!basicSwampInPlay && !basicIlsandInPlay)
                    {
                        foreach (Card carte in main)
                        {
                            if (carte.TapSansIle && carte.TapSansMarais)
                            {
                                return carte;
                            }
                        }
                    }
                    //tapland rouge noir si il arrive tappé
                    if (!basicSwampInPlay && !basicMoutainInPlay)
                    {
                        foreach (Card carte in main)
                        {
                            if (carte.TapSansMontage && carte.TapSansMarais)
                            {
                                return carte;
                            }
                        }
                    }

                    //tapland bleu noir
                    foreach (Card carte in main)
                    {
                        if (carte.TapSansIle && carte.TapSansMarais)
                        {
                            return carte;
                        }
                    }

                    //tapland bleu rouge si il nous manque 1 bleu
                    if (nbBleu < 2)
                    {
                        //fastland
                        foreach (Card carte in main)
                        {
                            if (carte.fastland)
                            {
                                return carte;
                            }
                        }

                        foreach (Card carte in main)
                        {
                            if (carte.TapSansIle && carte.TapSansMontage)
                            {
                                return carte;
                            }
                        }
                    }

                    //tapland rouge noir
                    foreach (Card carte in main)
                    {
                        if (carte.TapSansMontage && carte.TapSansMarais)
                        {
                            return carte;
                        }
                    }

                    //fastland
                    foreach (Card carte in main)
                    {
                        if (carte.fastland)
                        {
                            return carte;
                        }
                    }
                    //tapland bleu rouge
                    foreach (Card carte in main)
                    {
                        if (carte.TapSansIle && carte.TapSansMontage)
                        {
                            return carte;
                        }
                    }

                    //marais si ile ou montagne
                    if (basicIlsandInPlay || basicMoutainInPlay)
                    {
                        foreach (Card carte in main)
                        {
                            if (carte.basic && carte.marais)
                            {
                                return carte;
                            }
                        }
                    }

                    //ile si montagne ou marais
                    if (basicSwampInPlay || basicMoutainInPlay)
                    {
                        foreach (Card carte in main)
                        {
                            if (carte.basic && carte.ile)
                            {
                                return carte;
                            }
                        }
                    }

                    //montagne si ile ou marais
                    if (basicSwampInPlay || basicIlsandInPlay)
                    {
                        foreach (Card carte in main)
                        {
                            if (carte.basic && carte.montagne)
                            {
                                return carte;
                            }
                        }
                    }

                    //marais
                    foreach (Card carte in main)
                    {
                        if (carte.basic && carte.marais)
                        {
                            return carte;
                        }
                    }

                    //ile
                    foreach (Card carte in main)
                    {
                        if (carte.basic && carte.ile)
                        {
                            return carte;
                        }
                    }

                    //montagne
                    foreach (Card carte in main)
                    {
                        if (carte.basic && carte.montagne)
                        {
                            return carte;
                        }
                    }

                    //aether hub
                    foreach (Card carte in main)
                    {
                        if (carte.hub)
                        {
                            return carte;
                        }
                    }
                }

                if (nbTerrain == 5)
                {
                    if (nbRouge == 2)
                    {
                        //On essaye de jouer un rouge qui arrive untap
                        //Si en plus on peut ajouter le noir c'est la fête
                        if (basicMoutainInPlay || basicSwampInPlay)
                        {
                            foreach (Card carte in main)
                            {
                                if (carte.TapSansMontage && carte.TapSansMarais)
                                {
                                    return carte;
                                }
                            }
                        }

                        //Du rouge et du bleu, c'est pas mal aussi pour double bleu sauf si on a déjà double bleu
                        if (nbBleu > 1 && (basicIlsandInPlay || basicMoutainInPlay))
                        {
                            foreach (Card carte in main)
                            {
                                if (carte.TapSansIle && carte.TapSansMontage)
                                {
                                    return carte;
                                }
                            }
                        }

                        //montagne
                        foreach (Card carte in main)
                        {
                            if (carte.basic && carte.ile)
                            {
                                return carte;
                            }
                        }

                        //tapland bleu/rouge
                        if (basicIlsandInPlay || basicMoutainInPlay)
                        {
                            foreach (Card carte in main)
                            {
                                if (carte.TapSansIle && carte.TapSansMontage)
                                {
                                    return carte;
                                }
                            }
                        }

                        //hub
                        foreach (Card carte in main)
                        {
                            if (carte.hub)
                            {
                                return carte;
                            }
                        }
                    }
                    //on vise les 2 manas noirs
                    if (nbNoir == 1)
                    {
                        //On joue un land noir qui arrive untap

                        //tapland noir/bleu : on l'aurait joué au dessus si ça avait été possible

                        //tapland noir/rouge
                        if (basicMoutainInPlay || basicSwampInPlay)
                        {
                            foreach (Card carte in main)
                            {
                                if (carte.TapSansMontage && carte.TapSansMarais)
                                {
                                    return carte;
                                }
                            }
                        }

                        //marais
                        foreach (Card carte in main)
                        {
                            if (carte.basic && carte.marais)
                            {
                                return carte;
                            }
                        }

                        //hub
                        foreach (Card carte in main)
                        {
                            if (carte.hub)
                            {
                                return carte;
                            }
                        }
                    }
                    //Lands qui nous ajoutent une couleur bleue et qui arrivent dégagés
                    if (nbBleu == 1)
                    {
                        //Si c'est possible d'ajouter le noir parce qu'on a déjà rouge c'est cool
                        if (nbRouge > 0 && (basicSwampInPlay || basicMoutainInPlay))
                        {
                            foreach (Card carte in main)
                            {
                                if (carte.TapSansMarais && carte.TapSansMontage)
                                {
                                    return carte;
                                }
                            }
                        }

                        if (basicIlsandInPlay || basicMoutainInPlay)
                        {
                            foreach (Card carte in main)
                            {
                                if (carte.TapSansIle && carte.TapSansMontage)
                                {
                                    return carte;
                                }
                            }
                        }

                        if (basicSwampInPlay || basicIlsandInPlay)
                        {
                            foreach (Card carte in main)
                            {
                                if (carte.TapSansMarais && carte.TapSansIle)
                                {
                                    return carte;
                                }
                            }
                        }

                        foreach (Card carte in main)
                        {
                            if (carte.hub)
                            {
                                return carte;
                            }
                        }

                        foreach (Card carte in main)
                        {
                            if (carte.basic && carte.ile)
                            {
                                return carte;
                            }
                        }

                        //Si on peux avoir deux noirs untap on y va
                        if (nbNoir == 1)
                        {
                            //On joue un land noir qui arrive untap

                            //tapland noir/bleu : on l'aurait joué au dessus si ça avait été possible

                            //tapland noir/rouge
                            if (basicMoutainInPlay || basicSwampInPlay)
                            {
                                foreach (Card carte in main)
                                {
                                    if (carte.TapSansMontage && carte.TapSansMarais)
                                    {
                                        return carte;
                                    }
                                }
                            }

                            //marais
                            foreach (Card carte in main)
                            {
                                if (carte.basic && carte.marais)
                                {
                                    return carte;
                                }
                            }

                            //hub
                            foreach (Card carte in main)
                            {
                                if (carte.hub)
                                {
                                    return carte;
                                }
                            }
                        }
                    }
                    //Sinon, land qui arrive dégagé
                    else
                    {
                        //Si on a pas encore de rouge, il serait temps d'en avoir
                        if (nbRouge == 0)
                        {
                            //Si en plus on peut ajouter le noir c'est la fête
                            if (basicMoutainInPlay || basicSwampInPlay)
                            {
                                foreach (Card carte in main)
                                {
                                    if (carte.TapSansMontage && carte.TapSansMarais)
                                    {
                                        return carte;
                                    }
                                }
                            }

                            //Du rouge et du bleu, c'est pas mal aussi pour double bleu sauf si on a déjà double bleu
                            if (nbBleu > 1 && (basicIlsandInPlay || basicMoutainInPlay))
                            {
                                foreach (Card carte in main)
                                {
                                    if (carte.TapSansIle && carte.TapSansMontage)
                                    {
                                        return carte;
                                    }
                                }
                            }

                            //montagne
                            foreach (Card carte in main)
                            {
                                if (carte.basic && carte.ile)
                                {
                                    return carte;
                                }
                            }

                            //tapland bleu/rouge
                            if (basicIlsandInPlay || basicMoutainInPlay)
                            {
                                foreach (Card carte in main)
                                {
                                    if (carte.TapSansIle && carte.TapSansMontage)
                                    {
                                        return carte;
                                    }
                                }
                            }

                            //hub
                            foreach (Card carte in main)
                            {
                                if (carte.hub)
                                {
                                    return carte;
                                }
                            }
                        }
                        //si on a un bleu et un rouge, on essaye d'avoir un noir
                        else
                        {
                            //tapland noir/bleu
                            if (basicIlsandInPlay || basicSwampInPlay)
                            {
                                foreach (Card carte in main)
                                {
                                    if (carte.TapSansIle && carte.TapSansMarais)
                                    {
                                        return carte;
                                    }
                                }
                            }

                            //tapland noir/rouge
                            if (basicMoutainInPlay || basicSwampInPlay)
                            {
                                foreach (Card carte in main)
                                {
                                    if (carte.TapSansMontage && carte.TapSansMarais)
                                    {
                                        return carte;
                                    }
                                }
                            }

                            //marais
                            foreach (Card carte in main)
                            {
                                if (carte.basic && carte.marais)
                                {
                                    return carte;
                                }
                            }

                            //hub
                            foreach (Card carte in main)
                            {
                                if (carte.hub)
                                {
                                    return carte;
                                }
                            }

                            //tapland bleu/rouge
                            if (basicIlsandInPlay || basicMoutainInPlay)
                            {
                                foreach (Card carte in main)
                                {
                                    if (carte.TapSansIle && carte.TapSansMontage)
                                    {
                                        return carte;
                                    }
                                }
                            }

                            //ile
                            foreach (Card carte in main)
                            {
                                if (carte.basic && carte.ile)
                                {
                                    return carte;
                                }
                            }

                            //montagne
                            foreach (Card carte in main)
                            {
                                if (carte.basic && carte.montagne)
                                {
                                    return carte;
                                }
                            }

                        }
                    }

                    //Si on a pas moyen d'avoir quatre terrains dégagés, autant privilégier les lands qui arrivent engagés

                    //jouer un cycle land bleu/noir sauf si on a déjà du bleu et qu'on pourrait avoir du rouge
                    foreach (Card carte in main)
                    {
                        if (carte.tapped && carte.ile)
                        {
                            //on préfère jouer un cycle land rouge/noir si on a déjà du bleu
                            if (nbBleu > 0)
                            {
                                foreach (Card carte2 in main)
                                {
                                    if (carte2.tapped && carte2.montagne)
                                    {
                                        return carte2;
                                    }
                                }
                            }
                            return carte;
                        }
                    }

                    //jouer un cycle land rouge/noir
                    foreach (Card carte in main)
                    {
                        if (carte.tapped && carte.montagne)
                        {
                            return carte;
                        }
                    }

                    //tapland bleu noir si déjà du rouge
                    if (nbRouge > 0)
                    {
                        foreach (Card carte in main)
                        {
                            if (carte.TapSansIle && carte.TapSansMarais)
                            {
                                return carte;
                            }
                        }
                    }
                    //tapland rouge noir si déjà du bleu
                    if (nbBleu > 0)
                    {
                        foreach (Card carte in main)
                        {
                            if (carte.TapSansMontage && carte.TapSansMarais)
                            {
                                return carte;
                            }
                        }
                    }

                    //tapland bleu noir si il arrive tappé
                    if (!basicSwampInPlay && !basicIlsandInPlay)
                    {
                        foreach (Card carte in main)
                        {
                            if (carte.TapSansIle && carte.TapSansMarais)
                            {
                                return carte;
                            }
                        }
                    }
                    //tapland rouge noir si il arrive tappé
                    if (!basicSwampInPlay && !basicMoutainInPlay)
                    {
                        foreach (Card carte in main)
                        {
                            if (carte.TapSansMontage && carte.TapSansMarais)
                            {
                                return carte;
                            }
                        }
                    }

                    //tapland bleu noir
                    foreach (Card carte in main)
                    {
                        if (carte.TapSansIle && carte.TapSansMarais)
                        {
                            return carte;
                        }
                    }

                    //tapland bleu rouge si il nous manque 1 bleu
                    if (nbBleu < 2)
                    {
                        //fastland
                        foreach (Card carte in main)
                        {
                            if (carte.fastland)
                            {
                                return carte;
                            }
                        }

                        foreach (Card carte in main)
                        {
                            if (carte.TapSansIle && carte.TapSansMontage)
                            {
                                return carte;
                            }
                        }
                    }

                    //tapland rouge noir
                    foreach (Card carte in main)
                    {
                        if (carte.TapSansMontage && carte.TapSansMarais)
                        {
                            return carte;
                        }
                    }

                    //fastland
                    foreach (Card carte in main)
                    {
                        if (carte.fastland)
                        {
                            return carte;
                        }
                    }
                    //tapland bleu rouge
                    foreach (Card carte in main)
                    {
                        if (carte.TapSansIle && carte.TapSansMontage)
                        {
                            return carte;
                        }
                    }

                    //marais si ile ou montagne
                    if (basicIlsandInPlay || basicMoutainInPlay)
                    {
                        foreach (Card carte in main)
                        {
                            if (carte.basic && carte.marais)
                            {
                                return carte;
                            }
                        }
                    }

                    //ile si montagne ou marais
                    if (basicSwampInPlay || basicMoutainInPlay)
                    {
                        foreach (Card carte in main)
                        {
                            if (carte.basic && carte.ile)
                            {
                                return carte;
                            }
                        }
                    }

                    //montagne si ile ou marais
                    if (basicSwampInPlay || basicIlsandInPlay)
                    {
                        foreach (Card carte in main)
                        {
                            if (carte.basic && carte.montagne)
                            {
                                return carte;
                            }
                        }
                    }

                    //marais
                    foreach (Card carte in main)
                    {
                        if (carte.basic && carte.marais)
                        {
                            return carte;
                        }
                    }

                    //ile
                    foreach (Card carte in main)
                    {
                        if (carte.basic && carte.ile)
                        {
                            return carte;
                        }
                    }

                    //montagne
                    foreach (Card carte in main)
                    {
                        if (carte.basic && carte.montagne)
                        {
                            return carte;
                        }
                    }

                    //aether hub
                    foreach (Card carte in main)
                    {
                        if (carte.hub)
                        {
                            return carte;
                        }
                    }
                }
                return null;
            }

            public bool IsUntapped(Card terrain)
            {
                if (terrain.basic)
                {
                    return true;
                }

                if (terrain.hub)
                {
                    return true;
                }

                if (terrain.fastland && tour <= 3)
                {
                    return true;
                }

                if (terrain.TapSansIle && basicIlsandInPlay)
                {
                    return true;
                }

                if (terrain.TapSansMarais && basicSwampInPlay)
                {
                    return true;
                }

                if (terrain.TapSansMontage && basicMoutainInPlay)
                {
                    return true;
                }

                return false;
            }

            public void DrawCard(int nb)
            {
                for (int j = 1; j <= nb; j++)
                {
                    Card pioche = deck.First();
                    deck.Remove(pioche);
                    if (pioche.isLand)
                    {
                        main.Add(pioche);
                    }
                }
            }

            public int NbLandsEnMain()
            {
                int nb = 0;
                foreach (Card carte in main)
                {
                    if (carte.isLand)
                    {
                        nb++;
                    }
                }
                return nb;
            }

            public void Mulligan()
            {
                for (int j = 1; j <= main.Count; j++)
                {
                    Card pioche = main.First();
                    main.Remove(pioche);
                    deck.Add(pioche);
                }

                // on refill le deck avec les cartes useless
                for (int j = 1; deck.Count < 60; j++)
                {
                    Card card = new Card();
                    card.isLand = false;
                    deck.Add(card);
                }
                Shuffle();
            }


            public void PlayLand(Card land)
            {
                nbTerrain++;
                if (land.IsBlack())
                {
                    nbNoir++;
                }
                if (land.IsBlue())
                {
                    nbBleu++;
                }
                if (land.IsRed())
                {
                    nbRouge++;
                }
                if (land.marais)
                {
                    basicSwampInPlay = true;
                }
                if (land.montagne)
                {
                    basicMoutainInPlay = true;
                }
                if (land.ile)
                {
                    basicIlsandInPlay = true;
                }
                main.Remove(land);
            }
        }

        static void Main(string[] args)
        {

            int NrIterations = 1000000;
            bool SurLePlay = true;
            int nbcartes = 60;
            int failsSuccessifs = 0;
            bool modemanuel = false;
            bool modeChampionnat = false;
            bool modeDiscovey = true;

            //Non basic
            short aetherHub = -1;
            short SommetCraneDragon = -1;
            short CanyonCroupissant = -1;
            short SpireBlufCanal = -1;
            short ChuteSoufre = -1;
            short CatacombesNoyees = -1;
            short BassinFetides = -1;

            //Basic
            short ile = -1;
            short montagne = -1;
            short marais = -1;

            //Afficher la possibilité à l'utilisateur de rentrer une manabase
            Mode_Manuel:
            int choix;
            if (modemanuel)
            {
                choix = 1;
            }
            else if (modeChampionnat)
            {
                choix = 2;
            }
            else
            {
                Console.WriteLine("Si vous voulez rentrer une manabase, tapez 1. Si vous voulez lancer Championnat tapez 2. Pour Discovery, tapez autre chose");
                int.TryParse(Console.ReadLine(), out choix);
            }
            if (choix == 1)
            {
                modemanuel = true;
                modeDiscovey = false;
                while (aetherHub > 5 || aetherHub < 0)
                {
                    Console.WriteLine("nombre d'Aether hub entre 0 et 4");
                    int.TryParse(Console.ReadLine(), out choix);
                    aetherHub = (Int16)choix;
                }
                while (SommetCraneDragon > 5 || SommetCraneDragon < 0)
                {
                    Console.WriteLine("nombre de Sommet du CrâneDragon entre 0 et 4 (tapland noir/rouge)");
                    int.TryParse(Console.ReadLine(), out choix);
                    SommetCraneDragon = (Int16)choix;
                }
                while (CanyonCroupissant > 5 || CanyonCroupissant < 0)
                {
                    Console.WriteLine("nombre de Canyon Croupissant entre 0 et 4  (cycle land noir/rouge)");
                    int.TryParse(Console.ReadLine(), out choix);
                    CanyonCroupissant = (Int16)choix;
                }
                while (SpireBlufCanal > 5 || SpireBlufCanal < 0)
                {
                    Console.WriteLine("nombre de SpireBluf Canal entre 0 et 4 (fastland bleu/rouge)");
                    int.TryParse(Console.ReadLine(), out choix);
                    SpireBlufCanal = (Int16)choix;
                }
                while (ChuteSoufre > 5 || ChuteSoufre < 0)
                {
                    Console.WriteLine("nombre de Chute de Soufre entre 0 et 4 (tapland bleu/rouge)");
                    int.TryParse(Console.ReadLine(), out choix);
                    ChuteSoufre = (Int16)choix;
                }
                while (CatacombesNoyees > 5 || CatacombesNoyees < 0)
                {
                    Console.WriteLine("nombre de Catacombes Noyées entre 0 et 4 (tapland bleu/noir)");
                    int.TryParse(Console.ReadLine(), out choix);
                    CatacombesNoyees = (Int16)choix;
                }
                while (BassinFetides > 5 || BassinFetides < 0)
                {
                    Console.WriteLine("nombre de Bassin Fétides entre 0 et 4 (tapland noir/bleu)");
                    int.TryParse(Console.ReadLine(), out choix);
                    BassinFetides = (Int16)choix;
                }
                while (ile < 0)
                {
                    Console.WriteLine("nombre d'île");
                    int.TryParse(Console.ReadLine(), out choix);
                    ile = (Int16)choix;
                }
                while (montagne < 0)
                {
                    Console.WriteLine("nombre de montagne");
                    int.TryParse(Console.ReadLine(), out choix);
                    montagne = (Int16)choix;
                }
                while (marais < 0)
                {
                    Console.WriteLine("nombre de marais");
                    int.TryParse(Console.ReadLine(), out choix);
                    marais = (Int16)choix;
                }

                goto TesterCombinaison;

            }
            else if (choix == 2)
            {
                modeDiscovey = false;
                modeChampionnat = true;
                NrIterations = 50000000;
                //On boucle sur les meilleurs résultats de discovery jusqu'à ce qu'on en trouve un qui n'ai pas encore été décortiqué
                using (var db = new CombinaisonDbContext())
                {
                    Combinaison combi = (from c in db.Combinaisons
                                         where !db.Championnat.Any(o => o.aetherHub == c.aetherHub
                                            && o.SommetCraneDragon == c.SommetCraneDragon
                                            && o.CanyonCroupissant == c.CanyonCroupissant
                                            && o.ChuteSoufre == c.ChuteSoufre
                                            && o.CatacombesNoyees == c.CatacombesNoyees
                                            && o.SpireBlufCanal == c.SpireBlufCanal
                                            && o.BassinFetides == c.BassinFetides
                                            && o.ile == c.ile
                                            && o.montagne == c.montagne
                                            && o.marais == c.marais)
                                         select c).OrderByDescending(c => (double)c.score)
                                         .First();

                    aetherHub = combi.aetherHub;
                    SommetCraneDragon = combi.SommetCraneDragon;
                    CanyonCroupissant = combi.CanyonCroupissant;
                    SpireBlufCanal = combi.SpireBlufCanal;
                    ChuteSoufre = combi.ChuteSoufre;
                    CatacombesNoyees = combi.CatacombesNoyees;
                    BassinFetides = combi.BassinFetides;

                    //Basic
                    ile = combi.ile;
                    montagne = combi.montagne;
                    marais = combi.marais;
                }
               

                goto TesterCombinaison;
                

            }

                //Itérer sur les combinaisons :
                Initialiser:

            int totterrains = 0;
            int nbterraindansdeck = 24;

            //Non basic
            aetherHub = 0;
            SommetCraneDragon = 0;
            CanyonCroupissant = 0;
            SpireBlufCanal = 0;
            ChuteSoufre = 0;
            CatacombesNoyees = 0;
            BassinFetides = 0;

            //Basic
            ile = 0;
            montagne = 0;
            marais = 0;

            int rndom=0;
            //Générer une séquence aléatoire de 24 terrains

            //On choisit deux cartes que l'on ne va pas mettre
            //unused 1
            Random rnd = new Random();
            int unused1 = rnd.Next(1, 11);
            //unused 2
            int unused2 = rnd.Next(1, 11);
            while (unused1 == unused2)
            {
                unused2 = rnd.Next(1, 11);
            }
            while (true)
            {
                rndom = rnd.Next(1, 11);
                if (rndom != unused1 && rndom != unused2)
                { 
                    switch (rnd.Next(1, 11))
                    {
                        case (1):
                            if (aetherHub < 4)
                            {
                                aetherHub++;
                                totterrains++;
                            }
                            break;
                        case (2):
                            if (SommetCraneDragon < 4)
                            {
                                SommetCraneDragon++;
                                totterrains++;
                            }
                            break;
                        case (3):
                            if (CanyonCroupissant < 4)
                            {
                                CanyonCroupissant++;
                                totterrains++;
                            }
                            break;
                        case (4):
                            if (SpireBlufCanal < 4)
                            {
                                SpireBlufCanal++;
                                totterrains++;
                            }
                            break;
                        case (5):
                            if (ChuteSoufre < 4)
                            {
                                ChuteSoufre++;
                                totterrains++;
                            }
                            break;
                        case (6):
                            if (BassinFetides < 4)
                            {
                                BassinFetides++;
                                totterrains++;
                            }
                            break;
                        case (7):
                            ile++;
                            totterrains++;
                            break;
                        case (8):
                            montagne++;
                            totterrains++;
                            break;
                        case (9):
                            marais++;
                            totterrains++;
                            break;
                        case (10):
                            if (CatacombesNoyees < 4)
                            {
                                CatacombesNoyees++;
                                totterrains++;
                            }
                            break;
                        default:
                            break;
                    }
                }

                //Vérifier que la séquence n'est pas déjà prise
                if (totterrains == nbterraindansdeck)
                {

                    bool exist = false;
                    //tester que la série n'a pas déjà été testée
                    using (var db = new CombinaisonDbContext())
                    {
                        exist = db.Combinaisons
                        .Where(o => o.aetherHub == aetherHub
                        && o.SommetCraneDragon == SommetCraneDragon
                        && o.CanyonCroupissant == CanyonCroupissant
                        && o.ChuteSoufre == ChuteSoufre
                        && o.CatacombesNoyees == CatacombesNoyees
                        && o.SpireBlufCanal == SpireBlufCanal
                        && o.BassinFetides == BassinFetides
                        && o.ile == ile
                        && o.montagne == montagne
                        && o.marais == marais
                        )
                        .Count() > 0;
                    }

                    //Si elle a déjà été testée
                    if (exist)
                    {
                        failsSuccessifs++;
                        if (failsSuccessifs == 100)
                        {
                            goto fin;
                        }
                        goto Initialiser;
                    }
                    else
                    {
                        failsSuccessifs = 0;
                        goto TesterCombinaison;
                    }
                }
            }

            TesterCombinaison:
            //Objectifs principaux
            decimal UnLandTour1 = 0;
            decimal DeuxLandTour2 = 0;
            decimal TroisLandTour3 = 0;
            decimal QuatreLandTour4 = 0;
            decimal CinqLandTour5 = 0;
            decimal UnRougeTour2 = 0;
            decimal UnBleuTour3 = 0;
            decimal DeuxBleuTour5 = 0;
            decimal UnLandUntapTour1 = 0;
            decimal DeuxLandUntapTour2 = 0;
            decimal TroisLandUntapTour3 = 0;
            decimal QuatreLandUntapTour4 = 0;
            decimal CinqLandUntapTour5 = 0;

            decimal UnRetardBleuTour2 = 0;
            decimal unRetardRougeTour3 = 0;
            decimal UnRetardBleuTour3 = 0;
            decimal UnRetardBleuTour4 = 0;
            decimal unRetardRougeTour4 = 0;
            decimal TroisRougeTour4 = 0;
            decimal TroisRougeTour5 = 0;
            decimal UnRetardBleuTour5 = 0;
            decimal DeuxRetardBleusTour6 = 0;

            //Objectifs Bonus
            decimal UnBleuTour1 = 0;
            decimal UnBleuTour2 = 0;
            decimal DeuxNoirTour4 = 0;
            decimal UnBleuTour4 = 0;
            decimal unRougeTour3 = 0;
            decimal DeuxNoirTour5 = 0;
            decimal SansMulligan = 0;
            decimal Mulligan6 = 0;
            decimal Mulligan5 = 0;
            decimal Mulligan4 = 0;


            for (int i = 1; i <= NrIterations; i++)
            {

                Etat etat = new Etat();
                //Créer le deck
                //etat.SetDeck(int nbCartes, int ile, int montagne, int marais, int aetherHub, int SommetCraneDragon, int CanyonCroupissant, int SpireBlufCanal, int ChuteSoufre, int CatacombesNoyees, int BassinFetides);

                etat.SetDeck(nbcartes, ile, montagne, marais, aetherHub, SommetCraneDragon, CanyonCroupissant, SpireBlufCanal, ChuteSoufre, CatacombesNoyees, BassinFetides);


                //Piocher la main de départ
                etat.DrawCard(7);

                //Faire des mulligans
                int nbLandsEnMain = etat.NbLandsEnMain();
                if (nbLandsEnMain > 5 || nbLandsEnMain < 2)
                {
                    etat.Mulligan();
                    etat.DrawCard(6);
                    nbLandsEnMain = etat.NbLandsEnMain();
                    if (nbLandsEnMain > 5 || nbLandsEnMain < 2)
                    {
                        etat.Mulligan();
                        etat.DrawCard(5);
                        nbLandsEnMain = etat.NbLandsEnMain();
                        if (nbLandsEnMain > 4 || nbLandsEnMain < 1)
                        {
                            etat.Mulligan();
                            etat.DrawCard(4);
                            Mulligan4++;
                        }
                        else
                        {
                            Mulligan5++;
                        }
                    }
                    else
                    {
                        Mulligan6++;
                    }

                    //regard 1
                    //TODO ou pas car on garde aussi des mains non gardables avec des mains qui ne gagnent pas
                    // TODO : géréer le cas de plusieurs hubs
                    // Cycler un land pour trouver une autre couleur ou si on a trop de lands
                }
                else
                {
                    SansMulligan++;
                }

                //Tour1
                if (!SurLePlay)
                {
                    etat.DrawCard(1);
                }
                Card terrain = etat.ChoisirTerrain();
                if (terrain != null)
                {
                    etat.PlayLand(terrain);
                    if (etat.IsUntapped(terrain) && terrain.IsBlue())
                    {
                        UnBleuTour1++;
                    }
                    UnLandTour1++;
                    if (etat.IsUntapped(terrain))
                    {
                        UnLandUntapTour1++;
                    }
                }

                etat.tour++;

                //Tour2
                etat.DrawCard(1);
                terrain = etat.ChoisirTerrain();
                if (terrain != null)
                {
                    etat.PlayLand(terrain);
                    if (etat.IsUntapped(terrain) && etat.nbBleu >= 1 && etat.nbTerrain == 2)
                    {
                        UnBleuTour2++;
                    }
                    if (etat.IsUntapped(terrain) && etat.nbRouge >= 1 && etat.nbTerrain == 2)
                    {
                        UnRougeTour2++;
                    }
                    if (etat.nbTerrain == 2)
                    {
                        DeuxLandTour2++;
                    }
                    if (etat.IsUntapped(terrain) && etat.nbTerrain == 2)
                    {
                        DeuxLandUntapTour2++;
                    }
                    if ((etat.IsUntapped(terrain) && etat.nbBleu >= 1 && etat.nbTerrain > 0) || (etat.nbBleu >= 2 && etat.nbTerrain > 1) || (!terrain.IsBlue() && etat.nbBleu >= 1 && etat.nbTerrain > 1))
                    {
                        UnRetardBleuTour2++;
                    }
                }
                else
                {
                    if (etat.nbBleu >= 1 && etat.nbTerrain >= 1)
                    {
                        UnRetardBleuTour2++;
                    }
                }
                etat.tour++;

                //Tour3
                etat.DrawCard(1);
                terrain = etat.ChoisirTerrain();
                if (terrain != null)
                {
                    etat.PlayLand(terrain);
                    if (etat.IsUntapped(terrain) && etat.nbBleu >= 1 && etat.nbTerrain == 3)
                    {
                        UnBleuTour3++;
                    }
                    if (etat.IsUntapped(terrain) && etat.nbRouge >= 1 && etat.nbTerrain == 3)
                    {
                        unRougeTour3++;
                    }
                    if ((etat.IsUntapped(terrain) && etat.nbRouge >= 1 && etat.nbTerrain > 1) || (etat.nbRouge >= 2 && etat.nbTerrain > 2) || (!terrain.IsRed() && etat.nbRouge >= 1 && etat.nbTerrain > 2))
                    {
                        unRetardRougeTour3++;
                    }
                    if ((etat.IsUntapped(terrain) && etat.nbBleu >= 1 && etat.nbTerrain > 1) || (etat.nbBleu >= 2 && etat.nbTerrain > 2) || (!terrain.IsBlue() && etat.nbBleu >= 1 && etat.nbTerrain > 2))
                    {
                        UnRetardBleuTour3++;
                    }
                    if (etat.nbTerrain == 3)
                    {
                        TroisLandTour3++;
                    }
                    if (etat.IsUntapped(terrain) && etat.nbTerrain == 3)
                    {
                        TroisLandUntapTour3++;
                    }
                }
                else
                {
                    if (etat.nbRouge >= 1 && etat.nbTerrain >= 2)
                    {
                        unRetardRougeTour3++;
                    }
                    if (etat.nbBleu >= 1 && etat.nbTerrain >= 2)
                    {
                        UnRetardBleuTour3++;
                    }
                }
                etat.tour++;

                //Tour4
                etat.DrawCard(1);
                terrain = etat.ChoisirTerrain();
                if (terrain != null)
                {
                    etat.PlayLand(terrain);
                    if (etat.IsUntapped(terrain) && etat.nbNoir >= 2 && etat.nbTerrain == 4)
                    {
                        DeuxNoirTour4++;
                    }
                    if (etat.IsUntapped(terrain) && etat.nbBleu >= 1 && etat.nbTerrain == 4)
                    {
                        UnBleuTour4++;
                    }
                    if (etat.IsUntapped(terrain) && etat.nbRouge >= 3 && etat.nbTerrain == 4)
                    {
                        TroisRougeTour4++;
                    }
                    if ((etat.IsUntapped(terrain) && etat.nbBleu >= 1 && etat.nbTerrain > 2) || (etat.nbBleu >= 2 && etat.nbTerrain > 3) || (!terrain.IsBlue() && etat.nbBleu >= 1 && etat.nbTerrain > 3))
                    {
                        UnRetardBleuTour4++;
                    }
                    if ((etat.IsUntapped(terrain) && etat.nbRouge >= 1 && etat.nbTerrain > 2) || (etat.nbRouge >= 2 && etat.nbTerrain > 3) || (!terrain.IsRed() && etat.nbRouge >= 1 && etat.nbTerrain > 3))
                    {
                        unRetardRougeTour4++;
                    }
                    if (etat.nbTerrain == 4)
                    {
                        QuatreLandTour4++;
                    }
                    if (etat.IsUntapped(terrain) && etat.nbTerrain == 4)
                    {
                        QuatreLandUntapTour4++;
                    }
                }
                else
                {
                    if (etat.nbBleu >= 1 && etat.nbTerrain >= 3)
                    {
                        UnRetardBleuTour4++;
                    }
                    if (etat.nbRouge >= 1 && etat.nbTerrain >= 3)
                    {
                        unRetardRougeTour4++;
                    }
                }
                etat.tour++;

                //Tour5
                etat.DrawCard(1);
                terrain = etat.ChoisirTerrain();
                if (terrain != null)
                {
                    etat.PlayLand(terrain);
                    if (etat.IsUntapped(terrain) && etat.nbBleu >= 2 && etat.nbTerrain == 5)
                    {
                        DeuxBleuTour5++;
                    }
                    if ((etat.nbTerrain > 3 && etat.IsUntapped(terrain) && etat.nbRouge >= 3) || (etat.nbTerrain > 4 && etat.nbRouge >= 4) || (etat.nbTerrain > 4 && etat.nbRouge >= 3 && !terrain.IsRed()))
                    {
                        TroisRougeTour5++;
                    }
                    if ((etat.IsUntapped(terrain) && etat.nbBleu >= 1 && etat.nbTerrain > 3) || (etat.nbBleu >= 2 && etat.nbTerrain > 4) || (!terrain.IsBlue() && etat.nbBleu >= 1 && etat.nbTerrain > 4))
                    {
                        UnRetardBleuTour5++;
                    }
                    if ((etat.nbTerrain > 3 && etat.IsUntapped(terrain) && etat.nbNoir >= 2) || (etat.nbTerrain > 4 && etat.nbNoir >= 3) || (etat.nbTerrain > 4 && etat.nbNoir >= 2 && !terrain.IsBlack()))
                    {
                        DeuxNoirTour5++;
                    }
                    if (etat.nbTerrain == 5)
                    {
                        CinqLandTour5++;
                    }
                    if (etat.IsUntapped(terrain) && etat.nbTerrain == 5)
                    {
                        CinqLandUntapTour5++;
                    }
                }
                else
                {
                    if (etat.nbBleu >= 1 && etat.nbTerrain >= 4)
                    {
                        UnRetardBleuTour5++;
                    }
                    if (etat.nbNoir >= 2 && etat.nbTerrain >= 4)
                    {
                        DeuxNoirTour5++;
                    }
                }
                etat.tour++;

                //Tour6
                etat.DrawCard(1);
                terrain = etat.ChoisirTerrain();
                if (terrain != null)
                {
                    etat.PlayLand(terrain);
                    //if ((etat.nbTerrain > 4 && etat.IsUntapped(terrain) && etat.nbRouge >= 3) || (etat.nbTerrain > 5 && etat.nbRouge >= 4) || (etat.nbTerrain > 5 && etat.nbRouge >= 3 && !terrain.IsRed()))
                    //{
                    //    TroisRougeTour6++;
                    //}
                    //if ((etat.nbTerrain > 3 && etat.IsUntapped(terrain) && etat.nbNoir >= 2) || (etat.nbTerrain > 4 && etat.nbNoir >= 3) || (etat.nbTerrain > 4 && etat.nbNoir >= 2 && !terrain.IsBlack()))
                    //{
                    //    DeuxNoirTour6++;
                    //}
                    if ((etat.nbTerrain > 4 && etat.IsUntapped(terrain) && etat.nbBleu >= 2) || (etat.nbTerrain > 5 && etat.nbBleu >= 3) || (etat.nbTerrain > 5 && etat.nbBleu >= 2 && !terrain.IsBlue()))
                    {
                        DeuxRetardBleusTour6++;
                    }
                }
                else
                {
                    if (etat.nbBleu >= 2 && etat.nbTerrain >= 5)
                    {
                        DeuxRetardBleusTour6++;
                    }
                }

            } // end of iterations

            decimal? scorePrimaire = UnBleuTour1 * 2 + UnRougeTour2 * 8 + UnBleuTour3 * 6 + UnBleuTour2 * 2 + UnBleuTour4 * 4 + TroisRougeTour4 * 3 + DeuxNoirTour4 * 2 + DeuxBleuTour5 * 3;
            decimal? scoreSecondaire = (UnRetardBleuTour2 - UnBleuTour1) * 2 + (unRetardRougeTour3 - UnRougeTour2) * 8 + (UnRetardBleuTour3 - UnBleuTour2) * 2 + (UnRetardBleuTour4 - UnBleuTour3) * 6 + (UnRetardBleuTour5 - UnBleuTour4) * 4 + (unRetardRougeTour4 - unRougeTour3) * 1 + (TroisRougeTour5 - TroisRougeTour4) * 3 + (DeuxNoirTour5 - DeuxNoirTour4) * 2 + (DeuxRetardBleusTour6 - DeuxBleuTour5) * 3;
            decimal? score = (scorePrimaire + (decimal)0.5 * scoreSecondaire) / NrIterations;

            if (modeDiscovey)
            {
                using (var db = new CombinaisonDbContext())
                {
                    Combinaison nouvelleCombinaison = new Combinaison();
                    nouvelleCombinaison.score = score;
                    nouvelleCombinaison.score_primaire = scorePrimaire;
                    nouvelleCombinaison.score_secondaire = scoreSecondaire;
                    nouvelleCombinaison.aetherHub = aetherHub;
                    nouvelleCombinaison.SommetCraneDragon = SommetCraneDragon;
                    nouvelleCombinaison.CanyonCroupissant = CanyonCroupissant;
                    nouvelleCombinaison.ChuteSoufre = ChuteSoufre;
                    nouvelleCombinaison.CatacombesNoyees = CatacombesNoyees;
                    nouvelleCombinaison.SpireBlufCanal = SpireBlufCanal;
                    nouvelleCombinaison.BassinFetides = BassinFetides;
                    nouvelleCombinaison.ile = ile;
                    nouvelleCombinaison.montagne = montagne;
                    nouvelleCombinaison.marais = marais;
                    nouvelleCombinaison.UnLandTour1 = UnLandTour1 / NrIterations;
                    nouvelleCombinaison.DeuxLandTour2 = DeuxLandTour2 / NrIterations;
                    nouvelleCombinaison.TroisLandTour3 = TroisLandTour3 / NrIterations;
                    nouvelleCombinaison.QuatreLandTour4 = QuatreLandTour4 / NrIterations;
                    nouvelleCombinaison.CinqLandTour5 = CinqLandTour5 / NrIterations;
                    nouvelleCombinaison.UnRougeTour2 = UnRougeTour2 / NrIterations;
                    nouvelleCombinaison.UnBleuTour3 = UnBleuTour3 / NrIterations;
                    nouvelleCombinaison.DeuxBleuTour5 = DeuxBleuTour5 / NrIterations;
                    nouvelleCombinaison.UnRetardBleuTour2 = UnRetardBleuTour2 / NrIterations;
                    nouvelleCombinaison.unRetardRougeTour3 = unRetardRougeTour3 / NrIterations;
                    nouvelleCombinaison.UnRetardBleuTour3 = UnRetardBleuTour3 / NrIterations;
                    nouvelleCombinaison.UnRetardBleuTour4 = UnRetardBleuTour4 / NrIterations;
                    nouvelleCombinaison.unRetardRougeTour4 = unRetardRougeTour4 / NrIterations;
                    nouvelleCombinaison.UnBleuTour4 = UnBleuTour4 / NrIterations;
                    nouvelleCombinaison.unRougeTour3 = unRougeTour3 / NrIterations;
                    nouvelleCombinaison.TroisRougeTour4 = TroisRougeTour4 / NrIterations;
                    nouvelleCombinaison.TroisRougeTour5 = TroisRougeTour5 / NrIterations;
                    nouvelleCombinaison.UnRetardBleuTour5 = UnRetardBleuTour5 / NrIterations;
                    nouvelleCombinaison.DeuxRetardBleusTour6 = DeuxRetardBleusTour6 / NrIterations;
                    nouvelleCombinaison.UnLandUntapTour1 = UnLandUntapTour1 / NrIterations;
                    nouvelleCombinaison.DeuxLandUntapTour2 = DeuxLandUntapTour2 / NrIterations;
                    nouvelleCombinaison.TroisLandUntapTour3 = TroisLandUntapTour3 / NrIterations;
                    nouvelleCombinaison.QuatreLandUntapTour4 = QuatreLandUntapTour4 / NrIterations;
                    nouvelleCombinaison.CinqLandUntapTour5 = CinqLandUntapTour5 / NrIterations;
                    nouvelleCombinaison.UnBleuTour1 = UnBleuTour1 / NrIterations;
                    nouvelleCombinaison.UnBleuTour2 = UnBleuTour2 / NrIterations;
                    nouvelleCombinaison.DeuxNoirTour4 = DeuxNoirTour4 / NrIterations;
                    nouvelleCombinaison.DeuxNoirTour5 = DeuxNoirTour5 / NrIterations;
                    nouvelleCombinaison.SansMulligan = SansMulligan / NrIterations;
                    nouvelleCombinaison.Mulligan6 = Mulligan6 / NrIterations;
                    nouvelleCombinaison.Mulligan5 = Mulligan5 / NrIterations;
                    nouvelleCombinaison.Mulligan4 = Mulligan4 / NrIterations;
                    db.Combinaisons.Add(nouvelleCombinaison);
                    db.SaveChanges();
                }
            }
            
            if (modeChampionnat)
            {
                using (var db = new CombinaisonDbContext())
                {
                    Championnat nouvelleCombinaison = new Championnat();
                    nouvelleCombinaison.score = score;
                    nouvelleCombinaison.score_primaire = scorePrimaire;
                    nouvelleCombinaison.score_secondaire = scoreSecondaire;
                    nouvelleCombinaison.aetherHub = aetherHub;
                    nouvelleCombinaison.SommetCraneDragon = SommetCraneDragon;
                    nouvelleCombinaison.CanyonCroupissant = CanyonCroupissant;
                    nouvelleCombinaison.ChuteSoufre = ChuteSoufre;
                    nouvelleCombinaison.CatacombesNoyees = CatacombesNoyees;
                    nouvelleCombinaison.SpireBlufCanal = SpireBlufCanal;
                    nouvelleCombinaison.BassinFetides = BassinFetides;
                    nouvelleCombinaison.ile = ile;
                    nouvelleCombinaison.montagne = montagne;
                    nouvelleCombinaison.marais = marais;
                    nouvelleCombinaison.UnLandTour1 = UnLandTour1 / NrIterations;
                    nouvelleCombinaison.DeuxLandTour2 = DeuxLandTour2 / NrIterations;
                    nouvelleCombinaison.TroisLandTour3 = TroisLandTour3 / NrIterations;
                    nouvelleCombinaison.QuatreLandTour4 = QuatreLandTour4 / NrIterations;
                    nouvelleCombinaison.CinqLandTour5 = CinqLandTour5 / NrIterations;
                    nouvelleCombinaison.UnRougeTour2 = UnRougeTour2 / NrIterations;
                    nouvelleCombinaison.UnBleuTour3 = UnBleuTour3 / NrIterations;
                    nouvelleCombinaison.DeuxBleuTour5 = DeuxBleuTour5 / NrIterations;
                    nouvelleCombinaison.UnRetardBleuTour2 = UnRetardBleuTour2 / NrIterations;
                    nouvelleCombinaison.unRetardRougeTour3 = unRetardRougeTour3 / NrIterations;
                    nouvelleCombinaison.UnRetardBleuTour3 = UnRetardBleuTour3 / NrIterations;
                    nouvelleCombinaison.UnRetardBleuTour4 = UnRetardBleuTour4 / NrIterations;
                    nouvelleCombinaison.unRetardRougeTour4 = unRetardRougeTour4 / NrIterations;
                    nouvelleCombinaison.UnBleuTour4 = UnBleuTour4 / NrIterations;
                    nouvelleCombinaison.unRougeTour3 = unRougeTour3 / NrIterations;
                    nouvelleCombinaison.TroisRougeTour4 = TroisRougeTour4 / NrIterations;
                    nouvelleCombinaison.TroisRougeTour5 = TroisRougeTour5 / NrIterations;
                    nouvelleCombinaison.UnRetardBleuTour5 = UnRetardBleuTour5 / NrIterations;
                    nouvelleCombinaison.DeuxRetardBleusTour6 = DeuxRetardBleusTour6 / NrIterations;
                    nouvelleCombinaison.UnLandUntapTour1 = UnLandUntapTour1 / NrIterations;
                    nouvelleCombinaison.DeuxLandUntapTour2 = DeuxLandUntapTour2 / NrIterations;
                    nouvelleCombinaison.TroisLandUntapTour3 = TroisLandUntapTour3 / NrIterations;
                    nouvelleCombinaison.QuatreLandUntapTour4 = QuatreLandUntapTour4 / NrIterations;
                    nouvelleCombinaison.CinqLandUntapTour5 = CinqLandUntapTour5 / NrIterations;
                    nouvelleCombinaison.UnBleuTour1 = UnBleuTour1 / NrIterations;
                    nouvelleCombinaison.UnBleuTour2 = UnBleuTour2 / NrIterations;
                    nouvelleCombinaison.DeuxNoirTour4 = DeuxNoirTour4 / NrIterations;
                    nouvelleCombinaison.DeuxNoirTour5 = DeuxNoirTour5 / NrIterations;
                    nouvelleCombinaison.SansMulligan = SansMulligan / NrIterations;
                    nouvelleCombinaison.Mulligan6 = Mulligan6 / NrIterations;
                    nouvelleCombinaison.Mulligan5 = Mulligan5 / NrIterations;
                    nouvelleCombinaison.Mulligan4 = Mulligan4 / NrIterations;
                    db.Championnat.Add(nouvelleCombinaison);
                    db.SaveChanges();
                }
            }

            if (modemanuel || modeChampionnat)
            {
                Console.WriteLine("Score : " + score);
                goto Mode_Manuel;
            }
            

            goto Initialiser;

            fin:
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
