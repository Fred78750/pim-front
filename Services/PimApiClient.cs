using System.Net.Http.Json;
using System.Text.Json;
using PimFront.Models;

namespace PimFront.Services;

/// <summary>
/// Client HTTP vers api.vitajardin.com — scope read:public (clé sandbox).
/// Utilisé par la zone Public et les composants partagés.
/// </summary>
public class PimApiClient(HttpClient http, ILogger<PimApiClient> logger)
{
    private static readonly JsonSerializerOptions _json = new()
    {
        PropertyNameCaseInsensitive = true
    };

    // ─── Taxons ───────────────────────────────────────────────────────────

    public async Task<PagedResult<TaxonListItem>?> GetTaxonsAsync(
        int page = 1,
        int pageSize = 20,
        int? categorieId = null,
        CancellationToken ct = default)
    {
        var url = $"/api/v1/taxons?page={page}&page_size={pageSize}";
        if (categorieId.HasValue) url += $"&categorie_id={categorieId}";

        try
        {
            return await http.GetFromJsonAsync<PagedResult<TaxonListItem>>(url, _json, ct);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erreur GetTaxons page={Page}", page);
            return null;
        }
    }

    public async Task<TaxonDetail?> GetTaxonAsync(long id, CancellationToken ct = default)
    {
        try
        {
            return await http.GetFromJsonAsync<TaxonDetail>($"/api/v1/taxons/{id}", _json, ct);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erreur GetTaxon id={Id}", id);
            return null;
        }
    }

    // ─── Recherche ────────────────────────────────────────────────────────

    public async Task<SearchResponse?> SearchAsync(
        string q,
        int limit = 20,
        CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(q)) return null;

        try
        {
            return await http.GetFromJsonAsync<SearchResponse>(
                $"/api/v1/search?q={Uri.EscapeDataString(q)}&limit={limit}", _json, ct);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erreur Search q={Q}", q);
            return null;
        }
    }

    // ─── Catégories ───────────────────────────────────────────────────────

    public async Task<List<CategorieArborescence>?> GetArborescenceAsync(
        CancellationToken ct = default)
    {
        try
        {
            return await http.GetFromJsonAsync<List<CategorieArborescence>>(
                "/api/v1/categories/arborescence", _json, ct);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erreur GetArborescence");
            return null;
        }
    }
}

/// <summary>
/// Client HTTP vers api.vitajardin.com — scope read:full (clé interne).
/// Utilisé exclusivement par la zone Admin.
/// </summary>
public class PimAdminApiClient(HttpClient http, ILogger<PimAdminApiClient> logger)
{
    private static readonly JsonSerializerOptions _json = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public async Task<CompletudeStat?> GetCompletudAsync(CancellationToken ct = default)
    {
        try
        {
            return await http.GetFromJsonAsync<CompletudeStat>(
                "/api/v1/completude/stats", _json, ct);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erreur GetCompletude");
            return null;
        }
    }
}
