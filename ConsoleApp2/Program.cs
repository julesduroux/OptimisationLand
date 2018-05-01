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
                }
                for (int j = 1; j <= montagne; j++)
                {
                    Card card = new Card();
                    card.montagne = true;
                    card.basic = true;
                    deck.Add(card);
                }
                for (int j = 1; j <= marais; j++)
                {
                    Card card = new Card();
                    card.marais = true;
                    card.basic = true;
                    deck.Add(card);
                }
                for (int j = 1; j <= aetherHub; j++)
                {
                    Card card = new Card();
                    card.hub = true;
                    deck.Add(card);
                }
                for (int j = 1; j <= SommetCraneDragon; j++)
                {
                    Card card = new Card();
                    card.TapSansMontage = true;
                    card.TapSansMarais = true;
                    deck.Add(card);
                }
                for (int j = 1; j <= CanyonCroupissant; j++)
                {
                    Card card = new Card();
                    card.marais = true;
                    card.montagne = true;
                    card.tapped = true;
                    deck.Add(card);
                }
                for (int j = 1; j <= SpireBlufCanal; j++)
                {
                    Card card = new Card();
                    card.fastland = true;
                    deck.Add(card);
                }
                for (int j = 1; j <= ChuteSoufre; j++)
                {
                    Card card = new Card();
                    card.TapSansMontage = true;
                    card.TapSansIle = true;
                    deck.Add(card);
                }
                for (int j = 1; j <= CatacombesNoyees; j++)
                {
                    Card card = new Card();
                    card.TapSansIle = true;
                    card.TapSansMarais = true;
                    deck.Add(card);
                }
                for (int j = 1; j <= BassinFetides; j++)
                {
                    Card card = new Card();
                    card.marais = true;
                    card.ile = true;
                    card.tapped = true;
                    deck.Add(card);
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

            int NrIterations = 50000; //ajouter un 0
            bool SurLePlay = true;
            int nbcartes = 60;
            int failsSuccessifs = 0;

            //Itérer sur les combinaisons :

            Initialiser:

            int totterrains = 0;
            int nbterraindansdeck = 24;

            //Non basic
            int aetherHub = 0;
            int SommetCraneDragon = 0;
            int CanyonCroupissant = 0;
            int SpireBlufCanal = 0;
            int ChuteSoufre = 0;
            int CatacombesNoyees = 0;
            int BassinFetides = 0;

            //Basic
            int ile = 0;
            int montagne = 0;
            int marais = 0;

            //Générer une séquence aléatoire de 24 terrains
            while (true)
            {
                Random rnd = new Random();
                switch (rnd.Next(1, 10))
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
            double UnLandTour1 = 0;
            double DeuxLandTour2 = 0;
            double TroisLandTour3 = 0;
            double QuatreLandTour4 = 0;
            double CinqLandTour5 = 0;
            double UnRougeTour2 = 0;
            double UnBleuTour3 = 0;
            double DeuxBleuTour5 = 0;
            double DeuxNoirTour6 = 0;
            double TroisRougeTour6 = 0;
            double UnLandUntapTour1 = 0;
            double DeuxLandUntapTour2 = 0;
            double TroisLandUntapTour3 = 0;
            double QuatreLandUntapTour4 = 0;
            double CinqLandUntapTour5 = 0;

            //Objectifs Bonus
            double UnBleuTour1 = 0;
            double UnBleuTour2 = 0;
            double DeuxNoirTour4 = 0;
            double DeuxNoirTour5 = 0;
            double SansMulligan = 0;
            double Mulligan6 = 0;
            double Mulligan5 = 0;
            double Mulligan4 = 0;

            string filename = @"C:\Users\Jules\Desktop\60, 7, 5, 0, 4, 1, 3, 4, 0, 0, 0.txt";

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
                    if (etat.nbTerrain == 3)
                    {
                        TroisLandTour3++;
                    }
                    if (etat.IsUntapped(terrain) && etat.nbTerrain == 3)
                    {
                        TroisLandUntapTour3++;
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
                    if (etat.nbTerrain == 4)
                    {
                        QuatreLandTour4++;
                    }
                    if (etat.IsUntapped(terrain) && etat.nbTerrain == 4)
                    {
                        QuatreLandUntapTour4++;
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
                    if (etat.nbTerrain > 3 && ((etat.IsUntapped(terrain) && etat.nbNoir >= 2) || etat.nbNoir >= 3 || (etat.nbNoir >= 2 && !terrain.IsBlack())))
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
                etat.tour++;

                //Tour6
                etat.DrawCard(1);
                terrain = etat.ChoisirTerrain();
                if (terrain != null)
                {
                    etat.PlayLand(terrain);
                    if (etat.nbTerrain > 4 && ((etat.IsUntapped(terrain) && etat.nbRouge >= 2) || etat.nbNoir >= 4 || (etat.nbNoir >= 3 && !terrain.IsRed())))
                    {
                        TroisRougeTour6++;
                    }
                    if (etat.nbTerrain > 3 && ((etat.IsUntapped(terrain) && etat.nbNoir >= 2) || etat.nbNoir >= 3 || (etat.nbNoir >= 2 && !terrain.IsBlack())))
                    {
                        DeuxNoirTour6++;
                    }
                }


            } // end of iterations


            //using (System.IO.StreamWriter file = new System.IO.StreamWriter(filename, true))
            //{ 
            //    file.WriteLine("Requirements");
            //    file.WriteLine("UnLandTour1 : " + UnLandTour1 / NrIterations + " dont untap : " + UnLandUntapTour1/ NrIterations);
            //    file.WriteLine("DeuxLandTour2 : " + DeuxLandTour2 / NrIterations + " dont untap : " + DeuxLandUntapTour2 / NrIterations);
            //    file.WriteLine("TroisLandTour3 : " + TroisLandTour3 / NrIterations + " dont untap : " + TroisLandUntapTour3 / NrIterations);
            //    file.WriteLine("QuatreLandTour4 : " + QuatreLandTour4 / NrIterations + " dont untap : " + QuatreLandUntapTour4 / NrIterations);
            //    file.WriteLine("CinqLandTour5 : " + CinqLandTour5 / NrIterations + " dont untap : " + CinqLandUntapTour5 / NrIterations);
            //    file.WriteLine("UnRougeTour2 : " + UnRougeTour2 / NrIterations);
            //    file.WriteLine("DeuxBleuTour5 : " + DeuxBleuTour5 / NrIterations);
            //    file.WriteLine("DeuxNoirTour6 : " + DeuxNoirTour6 / NrIterations);
            //    file.WriteLine("TroisRougeTour6 : " + TroisRougeTour6 / NrIterations);

            //    file.WriteLine("Bonus");
            //    file.WriteLine("UnBleuTour1 : " + UnBleuTour1 / NrIterations);
            //    file.WriteLine("UnBleuTour2 : " + UnBleuTour2 / NrIterations);
            //    file.WriteLine("DeuxNoirTour4 : " + DeuxNoirTour4 / NrIterations);
            //    file.WriteLine("DeuxNoirTour5 : " + DeuxNoirTour5 / NrIterations);
            //    file.WriteLine("SansMulligan : " + SansMulligan / NrIterations);
            //    file.WriteLine("Mulligan6 : " + Mulligan6 / NrIterations);
            //    file.WriteLine("Mulligan5 : " + Mulligan5 / NrIterations);
            //    file.WriteLine("Mulligan4 : " + Mulligan4 / NrIterations);
            //}

            using (var db = new CombinaisonDbContext())
            {
                Combinaison nouvelleCombinaison = new Combinaison();
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
                UnLandTour1 = UnLandTour1 / NrIterations;
                DeuxLandTour2 = DeuxLandTour2 / NrIterations;
                TroisLandTour3 = TroisLandTour3 / NrIterations;
                QuatreLandTour4 = QuatreLandTour4 / NrIterations;
                CinqLandTour5 = CinqLandTour5 / NrIterations;
                UnRougeTour2 = UnRougeTour2 / NrIterations;
                UnBleuTour3 = UnBleuTour3 / NrIterations;
                DeuxBleuTour5 = DeuxBleuTour5 / NrIterations;
                DeuxNoirTour6 = DeuxNoirTour6 / NrIterations;
                TroisRougeTour6 = TroisRougeTour6 / NrIterations;
                UnLandUntapTour1 = UnLandUntapTour1 / NrIterations;
                DeuxLandUntapTour2 = DeuxLandUntapTour2 / NrIterations;
                TroisLandUntapTour3 = TroisLandUntapTour3 / NrIterations;
                QuatreLandUntapTour4 = QuatreLandUntapTour4 / NrIterations;
                CinqLandUntapTour5 = CinqLandUntapTour5 / NrIterations;
                UnBleuTour1 = UnBleuTour1 / NrIterations;
                UnBleuTour2 = UnBleuTour2 / NrIterations;
                DeuxNoirTour4 = DeuxNoirTour4 / NrIterations;
                DeuxNoirTour5 = DeuxNoirTour5 / NrIterations;
                SansMulligan = SansMulligan / NrIterations;
                Mulligan6 = Mulligan6 / NrIterations;
                Mulligan5 = Mulligan5 / NrIterations;
                Mulligan4 = Mulligan4 / NrIterations;
                db.Combinaisons.Add(nouvelleCombinaison);
                db.SaveChanges();
            }

            fin:
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
