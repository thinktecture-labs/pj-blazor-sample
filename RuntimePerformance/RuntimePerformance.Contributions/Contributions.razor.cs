using Grpc.Net.Client;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using ProtoBuf.Grpc.Client;
using RuntimePerformance.Shared.Models;
using RuntimePerformance.Shared.Services;

namespace RuntimePerformance.Contributions
{
    public partial class Contributions
    {
        [Inject] private GrpcChannel _channel { get; set; } = default!;

        private int _selectedRowNumber = -1;
        private MudTable<Contribution>? _mudTable;
        private List<Contribution> _contributions = new List<Contribution>();
        private IContributionsService? _contributionsService;
        protected override async Task OnInitializedAsync()
        {
            if (_channel != null)
            {
                _contributionsService = _channel.CreateGrpcService<IContributionsService>();
                _contributions = await _contributionsService.GetCollectionAsync(new CollectionRequest { SearchTerm = string.Empty, Take = int.MaxValue });
            }            
            await base.OnInitializedAsync();
        }

        private void RowClickEvent(TableRowClickEventArgs<Contribution> tableRowClickEventArgs)
        {
        }

        private string SelectedRowClassFunc(Contribution element, int rowNumber)
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