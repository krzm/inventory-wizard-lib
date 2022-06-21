using CLIReader;
using CLIWizardHelper;
using Inventory.Data;
using Serilog;

namespace Inventory.Wizard.Lib;

public class ItemCategoryInsertWizard 
    : InsertWizard<IInventoryUnitOfWork, Category>
{
    private readonly IReader<string> optionalTextReader;

    public ItemCategoryInsertWizard(
    IInventoryUnitOfWork unitOfWork
    , IReader<string> requiredTextReader
    , IReader<string> optionalTextReader
    , ILogger log)
        : base(unitOfWork, requiredTextReader, log)
    {
        this.optionalTextReader = optionalTextReader;
    }

    protected override Category GetEntity()
    {
        return new Category
        {
            Name = RequiredTextReader.Read(
                new ReadConfig(25, nameof(Category.Name)))
            ,
            Description = optionalTextReader.Read(
                new ReadConfig(70, nameof(Category.Description)))
            ,
            ParentId = GetId()
        };
    }

    private int? GetId()
    {
        try
        {
            var input = optionalTextReader.Read(
                new ReadConfig(6, nameof(Category.ParentId)));
            ArgumentNullException.ThrowIfNull(input);
            return int.Parse(input);
        }
        catch (ArgumentNullException ex)
        {
            Log.Error(ex, nameof(GetId));
            return null;
        }
    }

    protected override void InsertEntity(Category entity) => 
        UnitOfWork.Category.Insert(entity);
}