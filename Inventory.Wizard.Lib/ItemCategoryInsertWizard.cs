using CLIHelper;
using CLIReader;
using CLIWizardHelper;
using Inventory.Data;

namespace Inventory.Wizard.Lib;

public class ItemCategoryInsertWizard 
    : InsertWizard<IInventoryUnitOfWork, ItemCategory>
{
    private readonly IReader<string> optionalTextReader;

    public ItemCategoryInsertWizard(
    IInventoryUnitOfWork unitOfWork
    , IReader<string> requiredTextReader
    , IReader<string> optionalTextReader
    , IOutput output)
        : base(unitOfWork, requiredTextReader, output)
    {
        this.optionalTextReader = optionalTextReader;
    }

    protected override ItemCategory GetEntity()
    {
        return new ItemCategory
        {
            Name = RequiredTextReader.Read(
                new ReadConfig(25, nameof(ItemCategory.Name)))
            ,
            Description = optionalTextReader.Read(
                new ReadConfig(70, nameof(ItemCategory.Description)))
            ,
            ParentId = GetId()
        };
    }

    private int? GetId()
    {
        var input = optionalTextReader.Read(
            new ReadConfig(6, nameof(ItemCategory.ParentId)));
        var result = string.IsNullOrWhiteSpace(input) == false;
        return result ? int.Parse(input) : null;
    }

    protected override void InsertEntity(ItemCategory entity) => 
        UnitOfWork.ItemCategory.Insert(entity);
}