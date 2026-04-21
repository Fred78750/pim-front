using System.Text.Json.Serialization;

namespace PimFront.Models;

// ─── Taxon liste ──────────────────────────────────────────────────────────────
// Champs réels retournés par GET /api/v1/taxons

public record TaxonListItem(
    long Id,
    [property: JsonPropertyName("genreid")]          int? GenreId,
    [property: JsonPropertyName("espece")]            string? Espece,
    [property: JsonPropertyName("cultivar")]          string? Cultivar,
    [property: JsonPropertyName("categorieid")]       int? CategorieId,
    [property: JsonPropertyName("seoname")]           string? SeoName,
    [property: JsonPropertyName("statutconfiance")]   string? StatutConfiance,
    [property: JsonPropertyName("isactif")]           bool IsActif,
    [property: JsonPropertyName("genre_libelle")]     string? GenreLibelle,
    [property: JsonPropertyName("espece_label")]      string? EspeceLabel,
    [property: JsonPropertyName("sousespece_label")]  string? SousEspeceLabel,
    [property: JsonPropertyName("variete_label")]     string? VarieteLabel,
    [property: JsonPropertyName("nomcommercial")]     string? NomCommercial
);

// ─── Taxon détail ─────────────────────────────────────────────────────────────

public record TaxonDetail(
    long Id,
    string? DesignationLatine,
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
    [property: JsonPropertyName("page_size")] int PageSize
)
{
    public int TotalPages => Total == 0 ? 1 : (int)Math.Ceiling((double)Total / PageSize);
}

// ─── Catégories ───────────────────────────────────────────────────────────────

public record CategorieArborescence(
    int Id,
    [property: JsonPropertyName("titre")] string Libelle,
    string? SeoName,
    List<CategorieNiv2> Enfants
);

public record CategorieNiv2(
    int Id,
    [property: JsonPropertyName("titre")] string Libelle,
    string? SeoName
);

// ─── Recherche ────────────────────────────────────────────────────────────────

public record SearchHit(
    long Id,
    string Label,
    double Score,
    string Type,
    [property: JsonPropertyName("seo_name")]  string? SeoName,
    string? Categorie,
    [property: JsonPropertyName("taxon_id")] long? TaxonId
);

public record SearchResultGroups(
    List<SearchHit> Taxons,
    List<SearchHit> Synonymes,
    [property: JsonPropertyName("noms_commerciaux")] List<SearchHit> NomsCommerciaux,
    List<SearchHit> Categories
);

public record SearchResponse(
    string Query,
    [property: JsonPropertyName("total_hits")] int TotalHits,
    SearchResultGroups Results,
    [property: JsonPropertyName("duration_ms")] int DurationMs
);

// ─── Catégorie lookup (cache local) ──────────────────────────────────────────

public record CategorieRef(
    int Id,
    string Libelle,
    string? SeoName
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
