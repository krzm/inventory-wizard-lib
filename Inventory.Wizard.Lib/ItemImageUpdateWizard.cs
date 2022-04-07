using CLIReader;
using CLIWizardHelper;
using Inventory.Data;
using Serilog;

namespace Inventory.Wizard.Lib;

public class ItemImageUpdateWizard 
	: UpdateWizard<IInventoryUnitOfWork, ItemImage>
{
    public ItemImageUpdateWizard(
		IInventoryUnitOfWork unitOfWork
		, IReader<string> requiredTextReader
		, ILogger log) 
			: base(unitOfWork, requiredTextReader, log)
    {
    }

    protected override ItemImage GetById(int id)
    {
        return UnitOfWork.ItemImage.GetByID(id);
	}

    protected override void UpdateEntity(int nr, ItemImage model)
    {
        switch (nr)
        {
			case 1:
				var input = RequiredTextReader.Read(
					new ReadConfig(6, nameof(ItemImage.ItemId)));
				ArgumentNullException.ThrowIfNull(input);
				model.ItemId = int.Parse(input);
				break;
			case 2:
				model.Path = RequiredTextReader.Read(
					new ReadConfig(250, nameof(ItemImage.Path)));
				break;
		}
	}
}