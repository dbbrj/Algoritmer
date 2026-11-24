# Opgave 6 (12 %) – Binært søgetræ

**Opgave:** Ovenfor ses et binært søgetræ.

```
40
├── 20
│   ├── 10
│   │   ├── 5
│   │   │   ├── 1
│   │   │   └── 8
│   │   └── 15
│   └── 30
│       ├── 25
│       └── 35
│           └── (venstre) 33
└── 60
    ├── 50
    │   ├── 45
    │   └── 55
    └── 70
        ├── 65
        └── 75
```

- Hvilke yderligere egenskaber har dette træ?
- Hvad er træets *internal path length* med blade?
- Angiv nodernes rækkefølge i en *pre-order* traversering.

---

## Løsning

### Egenskaber

- **Gyldigt binært søgetræ:** For alle knuder gælder venstre < knude < højre. Fx ligger 33 mellem 30 og 35 og under 40.
- **Størrelse:** 18 knuder, heraf 9 blade (1, 8, 15, 25, 33, 45, 55, 65, 75).
- **Højde:** 4.
- **AVL-træ (balanceret):** For hver knude er højdeforskellen mellem venstre og højre undertræ højst 1:

  | Knude | h(venstre) | h(højre) | Forskel |
  |---|---:|---:|---:|
  | 40 | 3 (20) | 2 (60) | 1 |
  | 20 | 2 (10) | 2 (30) | 0 |
  | 10 | 1 (5) | 0 (15) | 1 |
  | 30 | 0 (25) | 1 (35) | 1 |
  | 35 | 0 (33) | −1 | 1 |
  | 5, 50, 60, 70 | | | 0 |

  Højden er derfor O(log N), og søgning, indsættelse og sletning er O(log N).
- **Ikke perfekt/komplet:** Nederste niveau har kun 3 af 16 mulige knuder, og de er ikke placeret længst til venstre (fx mangler 15's og 25's børn før 33).
- **Ikke fuldt (full):** 35 har kun ét barn.
- Niveau 0–3 er dog helt fyldt (1 + 2 + 4 + 8 = 15 knuder).

### Internal path length (med blade)

Summen af dybderne for alle knuder:

| Dybde | Knuder | Antal | Bidrag |
|---:|---|---:|---:|
| 0 | 40 | 1 | 0 |
| 1 | 20, 60 | 2 | 2 |
| 2 | 10, 30, 50, 70 | 4 | 8 |
| 3 | 5, 15, 25, 35, 45, 55, 65, 75 | 8 | 24 |
| 4 | 1, 8, 33 | 3 | 12 |

**Internal path length = 0 + 2 + 8 + 24 + 12 = 46**

### Pre-order (knude, venstre, højre)

**40, 20, 10, 5, 1, 8, 15, 30, 25, 35, 33, 60, 50, 45, 55, 70, 65, 75**
