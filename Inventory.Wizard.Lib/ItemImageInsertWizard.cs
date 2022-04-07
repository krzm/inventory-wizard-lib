using CLIReader;
using CLIWizardHelper;
using Inventory.Data;
using Serilog;

namespace Inventory.Wizard.Lib;

public class ItemImageInsertWizard 
	: InsertWizard<IInventoryUnitOfWork, ItemImage>
{
	public ItemImageInsertWizard(
	   IInventoryUnitOfWork unitOfWork
	   , IReader<string> requiredTextReader
	   , ILogger log)
		   : base(unitOfWork, requiredTextReader, log)
	{
	}

	protected override ItemImage GetEntity()
	{
		var input = RequiredTextReader.Read(new ReadConfig(6, nameof(ItemImage.ItemId)));
		ArgumentNullException.ThrowIfNull(input);
		return new ItemImage()
		{
			ItemId = int.Parse(input)
			,
			Path = RequiredTextReader.Read(new ReadConfig(250, nameof(ItemImage.Path)))
		};
	}

	protected override void InsertEntity(ItemImage entity) =>
		UnitOfWork.ItemImage.Insert(entity);
}