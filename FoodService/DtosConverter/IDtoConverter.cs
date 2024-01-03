namespace FoodService.Dto_sConverter
{
    public interface IDtoConverter<TEntity, TDto>
    where TEntity : class
    where TDto : class
    {
        TDto ToDto(TEntity entity);
        TEntity ToEntity(TDto dto);
    }
}
