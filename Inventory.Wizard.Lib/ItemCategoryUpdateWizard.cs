using CLIReader;
using CLIWizardHelper;
using Inventory.Data;
using Serilog;

namespace Inventory.Wizard.Lib;

public class ItemCategoryUpdateWizard 
	: UpdateWizard<IInventoryUnitOfWork, Category>
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

    protected override Category GetById(int id)
    {
        return UnitOfWork.Category.GetByID(id);
	}

    protected override void UpdateEntity(int nr, Category model)
    {
        switch (nr)
        {
			case 1:
				model.Name = RequiredTextReader.Read(
					new ReadConfig(25, nameof(Category.Name)));
				break;
			case 2:
				model.Description = optionalTextReader.Read(
					new ReadConfig(70, nameof(Category.Description)));
				break;
			case 3:
				try
				{
					var parentId = optionalTextReader.Read(
					new ReadConfig(6, nameof(Category.ParentId)));
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