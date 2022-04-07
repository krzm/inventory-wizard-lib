using CLIReader;
using CLIWizardHelper;
using Inventory.Data;
using Serilog;

namespace Inventory.Wizard.Lib;

public class ItemCategoryUpdateWizard 
	: UpdateWizard<IInventoryUnitOfWork, ItemCategory>
{
    private readonly IReader<string> optionalTextReader;

    public ItemCategoryUpdateWizard(
		IInventoryUnitOfWork unitOfWork
		, IReader<string> requiredTextReader
		, IReader<string> optionalTextReader
		, ILogger log) 
			: base(unitOfWork, requiredTextReader, log)
    {
        this.optionalTextReader = optionalTextReader;
    }

    protected override ItemCategory GetById(int id)
    {
        return UnitOfWork.ItemCategory.GetByID(id);
	}

    protected override void UpdateEntity(int nr, ItemCategory model)
    {
        switch (nr)
        {
			case 1:
				model.Name = RequiredTextReader.Read(
					new ReadConfig(25, nameof(ItemCategory.Name)));
				break;
			case 2:
				model.Description = optionalTextReader.Read(
					new ReadConfig(70, nameof(ItemCategory.Description)));
				break;
			case 3:
				try
				{
					var parentId = optionalTextReader.Read(
					new ReadConfig(6, nameof(ItemCategory.ParentId)));
					ArgumentNullException.ThrowIfNull(parentId);
					model.ParentId = int.Parse(parentId);
				}
				catch (ArgumentNullException)
				{
				}
				break;
		}
	}
}