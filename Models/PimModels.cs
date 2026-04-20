namespace PimFront.Models;

// ─── Taxon ───────────────────────────────────────────────────────────────────

public record TaxonListItem(
    long Id,
    string DesignationLatine,
    string? SeoH1,
    string? SeoName,
    string? NomCourant,
    int? CategorieId,
    string? CategorieLibelle,
    string? GenreLibelle,
    string? EspeceLibelle,
    string? Cultivar
);

public record TaxonDetail(
    long Id,
    string DesignationLatine,
    string? SeoH1,
    string? SeoName,
    string? NomCourant,
    int? CategorieId,
    string? CategorieLibelle,
    string? GenreLibelle,
    string? EspeceLibelle,
    string? Cultivar,
    string? TypeHorticulture,
    FicheBotaniqueRef? FicheBotanique,
    List<NomCommercialRef> NomsCommerciaux,
    List<SynonymeRef> Synonymes
);

public record FicheBotaniqueRef(long Id, string NomCourant, string? TypeFiche);
public record NomCommercialRef(long Id, string Libelle);
public record SynonymeRef(long Id, string Libelle, string Type);

// ─── Pagination ───────────────────────────────────────────────────────────────

public record PagedResult<T>(
    List<T> Items,
    int Total,
    int Page,
    int PageSize,
    int TotalPages
);

// ─── Catégories ───────────────────────────────────────────────────────────────

public record CategorieArborescence(
    int Id,
    string Libelle,
    string? SeoName,
    List<CategorieNiv2> Enfants
);

public record CategorieNiv2(
    int Id,
    string Libelle,
    string? SeoName,
    int NbTaxons
);

// ─── Recherche ────────────────────────────────────────────────────────────────

public record SearchResult(
    long Id,
    string DesignationLatine,
    string? SeoH1,
    string? CategorieLibelle,
    string? MatchSource,   // "designation" | "synonyme" | "nom_commercial"
    double? Score
);

// ─── Stats ────────────────────────────────────────────────────────────────────

public record CompletudeStat(
    int TotalTaxons,
    int AvecFiche,
    int AvecSeoH1,
    int AvecSeoName,
    int AvecCategorie,
    double PctFiche,
    double PctSeo
);
