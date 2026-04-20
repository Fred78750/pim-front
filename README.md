# pim-front — Frontend PIM Vitajardin

**Stack :** Blazor Server · .NET 8 · C#  
**API consommée :** `https://api.vitajardin.com`  
**Repo :** GitHub privé `Fred78250/pim-front`

---

## Structure

```
pim-front/
├── Areas/
│   ├── Admin/
│   │   ├── Dashboard/    ← Stats complétude (read:admin)
│   │   └── Taxons/       ← Liste + recherche (read:full)
│   └── Public/
│       ├── Catalogue/    ← Recherche + liste paginée (read:public)
│       └── Taxon/        ← Fiche détail taxon (read:public)
├── Models/
│   └── PimModels.cs      ← DTOs correspondant aux réponses API
├── Services/
│   └── PimApiClient.cs   ← Clients HTTP → api.vitajardin.com
├── Shared/
│   ├── Components/
│   │   └── TaxonCard.razor
│   └── MainLayout.razor
├── wwwroot/css/app.css
├── Program.cs
├── appsettings.json       ← BaseUrl + clés (via env vars en prod)
└── _Imports.razor
```

---

## Configuration

**Développement local** (`appsettings.Development.json`) :
```json
{
  "PimApi": {
    "BaseUrl": "http://localhost:8000",
    "Keys": {
      "Public": "ta_clé_sandbox",
      "Admin": "ta_clé_interne"
    }
  }
}
```

**Production** — injecter via variables d'environnement ECS :
```
PimApi__BaseUrl=https://api.vitajardin.com
PimApi__Keys__Public=<clé_sandbox>
PimApi__Keys__Admin=<clé_interne>
```

Ne jamais committer les vraies clés dans le repo.

---

## Lancer en local

```bash
dotnet restore
dotnet run
# → http://localhost:5000
```

---

## Pages disponibles

| Route | Zone | Scope API |
|---|---|---|
| `/catalogue` | Public | read:public |
| `/taxon/{id}` | Public | read:public |
| `/admin` | Admin | read:admin |
| `/admin/taxons` | Admin | read:full |

---

## Prochaines étapes (backlog)

- [ ] Auth admin (cookie ou Azure AD B2C)
- [ ] Page `/admin/taxons/{id}` — édition fiche
- [ ] Endpoint `GET /api/v1/taxons/{id}/categorie-resolue`
- [ ] Déploiement ECS Fargate eu-west-3
- [ ] Dockerfile + pipeline CI/CD GitHub Actions
