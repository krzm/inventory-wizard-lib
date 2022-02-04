using CLIHelper;
using CLIReader;
using CLIWizardHelper;
using Inventory.Data;

namespace Inventory.Wizard.Lib;

public class ItemImageUpdateWizard 
	: UpdateWizard<IInventoryUnitOfWork, ItemImage>
{
    public ItemImageUpdateWizard(
		IInventoryUnitOfWork unitOfWork
		, IReader<string> requiredTextReader
		, IOutput output) 
			: base(unitOfWork, requiredTextReader, output)
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
				model.ItemId = int.Parse(RequiredTextReader.Read(
					new ReadConfig(6, nameof(ItemImage.ItemId))));
				break;
			case 2:
				model.Path = RequiredTextReader.Read(
					new ReadConfig(250, nameof(ItemImage.Path)));
				break;
		}
	}
}