using CLIHelper;
using CLIReader;
using CLIWizardHelper;
using Inventory.Data;

namespace Inventory.Wizard.Lib;

public class ItemImageInsertWizard 
	: InsertWizard<IInventoryUnitOfWork, ItemImage>
{
	public ItemImageInsertWizard(
	   IInventoryUnitOfWork unitOfWork
	   , IReader<string> requiredTextReader
	   , IOutput output)
		   : base(unitOfWork, requiredTextReader, output)
	{
	}

	protected override ItemImage GetEntity()
	{
		return new ItemImage()
		{
			ItemId = int.Parse(RequiredTextReader.Read(new ReadConfig(6, nameof(ItemImage.ItemId))))
			,
			Path = RequiredTextReader.Read(new ReadConfig(250, nameof(ItemImage.Path)))
		};
	}

	protected override void InsertEntity(ItemImage entity) =>
		UnitOfWork.ItemImage.Insert(entity);
}