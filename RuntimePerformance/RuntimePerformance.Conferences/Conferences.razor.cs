using Grpc.Net.Client;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using ProtoBuf.Grpc.Client;
using RuntimePerformance.Shared.Models;
using RuntimePerformance.Shared.Services;

namespace RuntimePerformance.Conferences
{
    public partial class Conferences
    {
        [Inject] private GrpcChannel _channel { get; set; } = default!;

        private int _selectedRowNumber = -1;
        private MudTable<Conference>? _mudTable;
        private List<Conference> _conferences = new List<Conference>();
        private IConferenceService? _conferencesService;
        protected override async Task OnInitializedAsync()
        {
            if (_channel != null)
            {
                _conferencesService = _channel.CreateGrpcService<IConferenceService>();
                _conferences = await _conferencesService.GetCollectionAsync(new CollectionRequest { SearchTerm = string.Empty, Take = int.MaxValue });
            }
            await base.OnInitializedAsync();
        }

        private void RowClickEvent(TableRowClickEventArgs<Conference> tableRowClickEventArgs)
        {
        }

        private string SelectedRowClassFunc(Conference element, int rowNumber)
        {
            if (_selectedRowNumber == rowNumber)
            {
                _selectedRowNumber = -1;
                return string.Empty;
            }
            else if (_mudTable?.SelectedItem != null && _mudTable.SelectedItem.Id == element.Id)
            {
                _selectedRowNumber = rowNumber;
                return "selected";
            }
            else
            {
                return string.Empty;
            }
        }
    }
}