# Opgave 6 (10 %) – Binært søgetræ

**Opgave:** Ovenfor ses et binært søgetræ.

```
60
├── 41
│   ├── 16
│   │   └── (højre) 25
│   └── 53
│       ├── 46
│       │   └── (venstre) 42
│       └── 55
└── 74
    └── (venstre) 65
        ├── 63
        │   ├── 62
        │   └── 64
        └── 70
```

- Hvilke yderligere egenskaber har dette træ?
- Hvad er træets *internal path length* med og uden blade?
- Angiv nodernes rækkefølge i en *post-order* traversering.

---

## Løsning

### Egenskaber

- **Gyldigt binært søgetræ:** For alle knuder gælder venstre < knude < højre. Fx ligger 42, 46 og 55 mellem 41 og 60, og 62, 64 og 70 ligger mellem 60 og 74.
- **Størrelse:** 14 knuder, heraf 6 blade (25, 42, 55, 62, 64, 70) og 8 interne knuder.
- **Højde:** 4 (længste sti fra roden, fx 60–41–53–46–42).
- **Ikke balanceret (ikke AVL):** Knude 74 har venstre undertræ (65) med højde 2 og intet højre undertræ (højde −1). Forskellen 3 > 1. Resten af træet overholder AVL-betingelsen; det er kun 74, der bryder den.
- **Ikke fuldt (full):** 16, 46 og 74 har kun ét barn.
- **Ikke komplet:** Fx mangler 74's højre barn, mens niveau 3 og 4 har knuder.
- Kan heller ikke farves som et rød-sort træ: under 74 er der en sti med 0 knuder til højre og en med 3 til venstre, og det kan ikke give samme sorthøjde uden to røde i træk.

### Internal path length

Internal path length = summen af dybderne for alle knuder.

| Dybde | Knuder | Antal | Bidrag |
|---:|---|---:|---:|
| 0 | 60 | 1 | 0 |
| 1 | 41, 74 | 2 | 2 |
| 2 | 16, 53, 65 | 3 | 6 |
| 3 | 25, 46, 55, 63, 70 | 5 | 15 |
| 4 | 42, 62, 64 | 3 | 12 |

- **Med blade:** 0 + 2 + 6 + 15 + 12 = **35**
- **Uden blade:** Bladene (25, 55, 70 på dybde 3 og 42, 62, 64 på dybde 4) bidrager 3·3 + 3·4 = 21. Altså 35 − 21 = **14**.
  (Kontrol: 60(0) + 41(1) + 74(1) + 16(2) + 53(2) + 65(2) + 46(3) + 63(3) = 14 ✓)

### Post-order (venstre, højre, knude)

**25, 16, 42, 46, 55, 53, 41, 62, 64, 63, 70, 65, 74, 60**
