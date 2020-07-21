using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using AutoMapper;

using Mealmate.Application.Interfaces;
using Mealmate.Application.Models;
using Mealmate.Core.Entities;
using Mealmate.Core.Interfaces;
using Mealmate.Core.Paging;
using Mealmate.Core.Repositories;
using Mealmate.Core.Specifications;
using Mealmate.Infrastructure.Paging;

namespace Mealmate.Application.Services
{
    public class TableService : ITableService
    {
        private readonly ITableRepository _tableRepository;
        private readonly IAppLogger<TableService> _logger;
        private readonly IMapper _mapper;

        public TableService(
            ITableRepository tableRepository,
            IAppLogger<TableService> logger,
            IMapper mapper)
        {
            _tableRepository = tableRepository ?? throw new ArgumentNullException(nameof(tableRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper;
        }

        public async Task<TableModel> Create(TableCreateModel model)
        {
            var newtable = new Table
            {
                Name = model.Name,
                IsActive = model.IsActive,
                Created = DateTime.Now,
                LocationId = model.LocationId
            };

            newtable = await _tableRepository.SaveAsync(newtable);

            _logger.LogInformation("entity successfully added - mealmateappservice");

            var newtablemodel = _mapper.Map<TableModel>(newtable);
            return newtablemodel;
        }

        public async Task Delete(int id)
        {
            var existingTable = await _tableRepository.GetByIdAsync(id);
            if (existingTable == null)
            {
                throw new ApplicationException("Table with this id is not exists");
            }

            existingTable.IsActive = false;
            await _tableRepository.SaveAsync(existingTable);

            _logger.LogInformation("Entity successfully updated - MealmateAppService");
        }

        public async Task<IEnumerable<TableModel>> Get(int locationId)
        {
            var result = await _tableRepository.GetAsync(x => x.LocationId == locationId);
            return _mapper.Map<IEnumerable<TableModel>>(result);
        }

        public async Task<TableModel> GetById(int id)
        {
            return _mapper.Map<TableModel>(await _tableRepository.GetByIdAsync(id));
        }

        public async Task Update(int id, TableUpdateModel model)
        {
            var existingTable = await _tableRepository.GetByIdAsync(id);
            if (existingTable == null)
            {
                throw new ApplicationException("Table with this id is not exists");
            }

            existingTable.Name = model.Name;
            existingTable.IsActive = model.IsActive;

            await _tableRepository.SaveAsync(existingTable);

            _logger.LogInformation("Entity successfully updated - MealmateAppService");
        }

        public async Task<IPagedList<TableModel>> Search(PageSearchArgs args)
        {
            var TablePagedList = await _tableRepository.SearchAsync(args);

            //TODO: PagedList<TSource> will be mapped to PagedList<TDestination>;
            var AllergenModels = _mapper.Map<List<TableModel>>(TablePagedList.Items);

            var AllergenModelPagedList = new PagedList<TableModel>(
                TablePagedList.PageIndex,
                TablePagedList.PageSize,
                TablePagedList.TotalCount,
                TablePagedList.TotalPages,
                AllergenModels);

            return AllergenModelPagedList;
        }

        public async Task<IPagedList<TableModel>> Search(int locationId, int isActive, PageSearchArgs args)
        {
            var TablePagedList = await _tableRepository.SearchAsync(locationId, isActive, args);

            //TODO: PagedList<TSource> will be mapped to PagedList<TDestination>;
            var AllergenModels = _mapper.Map<List<TableModel>>(TablePagedList.Items);

            var AllergenModelPagedList = new PagedList<TableModel>(
                TablePagedList.PageIndex,
                TablePagedList.PageSize,
                TablePagedList.TotalCount,
                TablePagedList.TotalPages,
                AllergenModels);

            return AllergenModelPagedList;
        }

        //public async Task<IEnumerable<TableModel>> GetTableList()
        //{
        //    var TableList = await _tableRepository.ListAllAsync();

        //    var TableModels = ObjectMapper.Mapper.Map<IEnumerable<TableModel>>(TableList);

        //    return TableModels;
        //}

        //public async Task<IPagedList<TableModel>> SearchTables(PageSearchArgs args)
        //{
        //    var TablePagedList = await _tableRepository.SearchTablesAsync(args);

        //    //TODO: PagedList<TSource> will be mapped to PagedList<TDestination>;
        //    var TableModels = ObjectMapper.Mapper.Map<List<TableModel>>(TablePagedList.Items);

        //    var TableModelPagedList = new PagedList<TableModel>(
        //        TablePagedList.PageIndex,
        //        TablePagedList.PageSize,
        //        TablePagedList.TotalCount,
        //        TablePagedList.TotalPages,
        //        TableModels);

        //    return TableModelPagedList;
        //}

        //public async Task<TableModel> GetTableById(int TableId)
        //{
        //    var Table = await _tableRepository.GetByIdAsync(TableId);

        //    var TableModel = ObjectMapper.Mapper.Map<TableModel>(Table);

        //    return TableModel;
        //}

        //public async Task<IEnumerable<TableModel>> GetTablesByName(string name)
        //{
        //    var spec = new TableWithTableesSpecification(name);
        //    var TableList = await _tableRepository.GetAsync(spec);

        //    var TableModels = ObjectMapper.Mapper.Map<IEnumerable<TableModel>>(TableList);

        //    return TableModels;
        //}

        //public async Task<IEnumerable<TableModel>> GetTablesByCategoryId(int categoryId)
        //{
        //    var spec = new TableWithTableesSpecification(categoryId);
        //    var TableList = await _tableRepository.GetAsync(spec);

        //    var TableModels = ObjectMapper.Mapper.Map<IEnumerable<TableModel>>(TableList);

        //    return TableModels;
        //}

        //public async Task<TableModel> CreateTable(TableModel Table)
        //{
        //    var existingTable = await _tableRepository.GetByIdAsync(Table.Id);
        //    if (existingTable != null)
        //    {
        //        throw new ApplicationException("Table with this id already exists");
        //    }

        //    var newTable = ObjectMapper.Mapper.Map<Table>(Table);
        //    newTable = await _tableRepository.SaveAsync(newTable);

        //    _logger.LogInformation("Entity successfully added - MealmateAppService");

        //    var newTableModel = ObjectMapper.Mapper.Map<TableModel>(newTable);
        //    return newTableModel;
        //}

        //public async Task UpdateTable(TableModel Table)
        //{
        //    var existingTable = await _tableRepository.GetByIdAsync(Table.Id);
        //    if (existingTable == null)
        //    {
        //        throw new ApplicationException("Table with this id is not exists");
        //    }

        //    existingTable.Name = Table.Name;
        //    existingTable.Description = Table.Description;

        //    await _tableRepository.SaveAsync(existingTable);

        //    _logger.LogInformation("Entity successfully updated - MealmateAppService");
        //}

        //public async Task DeleteTableById(int TableId)
        //{
        //    var existingTable = await _tableRepository.GetByIdAsync(TableId);
        //    if (existingTable == null)
        //    {
        //        throw new ApplicationException("Table with this id is not exists");
        //    }

        //    await _tableRepository.DeleteAsync(existingTable);

        //    _logger.LogInformation("Entity successfully deleted - MealmateAppService");
        //}
    }
}
