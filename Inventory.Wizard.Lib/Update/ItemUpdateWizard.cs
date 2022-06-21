using CLIReader;
using CLIWizardHelper;
using Inventory.Data;
using Serilog;

namespace Inventory.Wizard.Lib;

public class ItemUpdateWizard 
	: UpdateWizard<IInventoryUnitOfWork, Item>
{
    public ItemUpdateWizard(
		IInventoryUnitOfWork unitOfWork
		, IReader<string> requiredTextReader
		, ILogger log) 
			: base(unitOfWork, requiredTextReader, log)
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
				var input = RequiredTextReader.Read(
					new ReadConfig(6, nameof(Item.CategoryId)));
				ArgumentNullException.ThrowIfNull(input);
				model.CategoryId = int.Parse(input);
				break;
		}
	}
}