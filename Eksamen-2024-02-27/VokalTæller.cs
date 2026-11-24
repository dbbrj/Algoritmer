/*
 * Reeksamen 27. februar 2024 – Opgave 3 (18 %)
 *
 * Opgaven:
 *   Skriv en rekursiv metode/algoritme med følgende signatur:
 *
 *     int antalVokaler(String str, int l)
 *
 *   Parameteren str indeholder små bogstaver i det engelske alfabet:
 *   {a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z}.
 *   Og metoden skal returnere antallet af vokaler i strengen str, og vi
 *   antager, at vokalerne er: {a,e,i,o,u,y}
 *
 *   Kaldt med strengen "stationsbygninger" returneres 6, eftersom vokalerne i
 *   strengen er "a,i,o,y,i,e".
 *
 *   Den anden parameter i metoden angiver længden af strengen minus 1, idet
 *   det antages, at strengen opfattes som et array af karakterer (chars).
 *
 *   Det kan anbefales at skrive en hjælpemetode med returværdi true ellers
 *   false, som kan afgøre om den enlige parameter af datatypen char, er en
 *   vokal eller en konsonant.
 */
public class VokalTæller
{
    // Kaldes med AntalVokaler(str, str.Length - 1).
    // l er indekset på det sidste tegn, der endnu ikke er talt med.
    // Basistilfælde: l < 0 -> ingen tegn tilbage -> 0 vokaler.
    // Rekursivt tilfælde: (1 hvis str[l] er vokal, ellers 0) + vokalerne i str[0..l-1].
    // N+1 kald med konstant arbejde -> O(N).
    public int AntalVokaler(string str, int l)
    {
        if (l < 0)
            return 0;
        int her = ErVokal(str[l]) ? 1 : 0;
        return her + AntalVokaler(str, l - 1);
    }

    // Hjælpemetoden: true hvis c er en vokal (a, e, i, o, u, y), ellers false.
    public bool ErVokal(char c)
    {
        switch (c)
        {
            case 'a':
            case 'e':
            case 'i':
            case 'o':
            case 'u':
            case 'y':
                return true;
            default:
                return false;
        }
    }
}
