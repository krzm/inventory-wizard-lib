using CLIHelper;
using CLIReader;
using CLIWizardHelper;
using Inventory.Data;

namespace Inventory.Wizard.Lib;

public class ItemUpdateWizard 
	: UpdateWizard<IInventoryUnitOfWork, Item>
{
    public ItemUpdateWizard(
		IInventoryUnitOfWork unitOfWork
		, IReader<string> requiredTextReader
		, IOutput output) 
			: base(unitOfWork, requiredTextReader, output)
    {
    }

    protected override Item GetById(int id)
    {
		return UnitOfWork.Item.GetByID(id);
	}

	protected override void UpdateEntity(int nr, Item model)
    {
		switch (nr)
		{
			case 1:
				model.Name = RequiredTextReader.Read(
					new ReadConfig(25, nameof(Item.Name)));
				break;
			case 2:
				model.ItemCategoryId = int.Parse(RequiredTextReader.Read(
					new ReadConfig(6, nameof(Item.ItemCategoryId))));
				break;
		}
	}
}