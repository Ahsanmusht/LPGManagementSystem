using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using LPGManagementSystem.Models;
using LPGManagementSystem.DTO;
using LPGManagementSystem.Repository;
using LPGManagementSystem.Data;

namespace LPGManagementSystem.Controllers
{
    // ========== PartyType Controller ==========
    [ApiController]
    [Route("api/[controller]")]
    public class PartyTypeController : ControllerBase
    {
        private readonly IGenericRepository<PartyType> _repository;
        private readonly IMapper _mapper;

        public PartyTypeController(IGenericRepository<PartyType> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<PartyTypeDto>>>> GetAll()
        {
            try
            {
                var partyTypes = await _repository.GetAllAsync();
                var dtos = _mapper.Map<IEnumerable<PartyTypeDto>>(partyTypes);
                return Ok(ApiResponse<IEnumerable<PartyTypeDto>>.SuccessResponse(dtos));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<IEnumerable<PartyTypeDto>>.ErrorResponse(ex.Message));
            }
        }
    }

    // ========== Party Controller ==========
    [ApiController]
    [Route("api/[controller]")]
    public class PartyController : ControllerBase
    {
        private readonly IPartyRepository _repository;
        private readonly IMapper _mapper;

        public PartyController(IPartyRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<PartyDto>>>> GetAll()
        {
            try
            {
                var parties = await _repository.GetPartiesWithTypeAsync();
                var dtos = _mapper.Map<IEnumerable<PartyDto>>(parties);
                return Ok(ApiResponse<IEnumerable<PartyDto>>.SuccessResponse(dtos));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<IEnumerable<PartyDto>>.ErrorResponse(ex.Message));
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<PartyDto>>> GetById(int id)
        {
            try
            {
                var party = await _repository.GetPartyWithTypeAsync(id);
                if (party == null)
                    return NotFound(ApiResponse<PartyDto>.ErrorResponse("Party not found"));

                var dto = _mapper.Map<PartyDto>(party);
                return Ok(ApiResponse<PartyDto>.SuccessResponse(dto));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<PartyDto>.ErrorResponse(ex.Message));
            }
        }

        [HttpGet("bytype/{partyTypeId}")]
        public async Task<ActionResult<ApiResponse<IEnumerable<PartyDto>>>> GetByPartyType(int partyTypeId)
        {
            try
            {
                var parties = await _repository.GetPartiesByTypeAsync(partyTypeId);
                var dtos = _mapper.Map<IEnumerable<PartyDto>>(parties);
                return Ok(ApiResponse<IEnumerable<PartyDto>>.SuccessResponse(dtos));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<IEnumerable<PartyDto>>.ErrorResponse(ex.Message));
            }
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<PartyDto>>> Create([FromBody] CreatePartyDto dto)
        {
            try
            {
                var party = _mapper.Map<Party>(dto);
                await _repository.AddAsync(party);
                
                var createdParty = await _repository.GetPartyWithTypeAsync(party.Id);
                var resultDto = _mapper.Map<PartyDto>(createdParty);
                return CreatedAtAction(nameof(GetById), new { id = party.Id }, 
                    ApiResponse<PartyDto>.SuccessResponse(resultDto, "Party created successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<PartyDto>.ErrorResponse(ex.Message));
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<PartyDto>>> Update(int id, [FromBody] UpdatePartyDto dto)
        {
            try
            {
                var existingParty = await _repository.GetByIdAsync(id);
                if (existingParty == null)
                    return NotFound(ApiResponse<PartyDto>.ErrorResponse("Party not found"));

                _mapper.Map(dto, existingParty);
                await _repository.UpdateAsync(existingParty);

                var updatedParty = await _repository.GetPartyWithTypeAsync(id);
                var resultDto = _mapper.Map<PartyDto>(updatedParty);
                return Ok(ApiResponse<PartyDto>.SuccessResponse(resultDto, "Party updated successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<PartyDto>.ErrorResponse(ex.Message));
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> Delete(int id)
        {
            try
            {
                var party = await _repository.GetByIdAsync(id);
                if (party == null)
                    return NotFound(ApiResponse<bool>.ErrorResponse("Party not found"));

                await _repository.DeleteAsync(party);
                return Ok(ApiResponse<bool>.SuccessResponse(true, "Party deleted successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<bool>.ErrorResponse(ex.Message));
            }
        }
    }

    // ========== Item Controller ==========
    [ApiController]
    [Route("api/[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly IItemRepository _repository;
        private readonly IMapper _mapper;

        public ItemController(IItemRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<ItemDto>>>> GetAll()
        {
            try
            {
                var items = await _repository.GetAllAsync();
                var dtos = _mapper.Map<IEnumerable<ItemDto>>(items);
                return Ok(ApiResponse<IEnumerable<ItemDto>>.SuccessResponse(dtos));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<IEnumerable<ItemDto>>.ErrorResponse(ex.Message));
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<ItemDto>>> GetById(int id)
        {
            try
            {
                var item = await _repository.GetByIdAsync(id);
                if (item == null)
                    return NotFound(ApiResponse<ItemDto>.ErrorResponse("Item not found"));

                var dto = _mapper.Map<ItemDto>(item);
                return Ok(ApiResponse<ItemDto>.SuccessResponse(dto));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<ItemDto>.ErrorResponse(ex.Message));
            }
        }

        [HttpGet("primary")]
        public async Task<ActionResult<ApiResponse<ItemDto>>> GetPrimary()
        {
            try
            {
                var item = await _repository.GetPrimaryItemAsync();
                if (item == null)
                    return NotFound(ApiResponse<ItemDto>.ErrorResponse("Primary item not found"));

                var dto = _mapper.Map<ItemDto>(item);
                return Ok(ApiResponse<ItemDto>.SuccessResponse(dto));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<ItemDto>.ErrorResponse(ex.Message));
            }
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<ItemDto>>> Create([FromBody] CreateItemDto dto)
        {
            try
            {
                var item = _mapper.Map<Item>(dto);
                await _repository.AddAsync(item);

                var resultDto = _mapper.Map<ItemDto>(item);
                return CreatedAtAction(nameof(GetById), new { id = item.ItemId },
                    ApiResponse<ItemDto>.SuccessResponse(resultDto, "Item created successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<ItemDto>.ErrorResponse(ex.Message));
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<ItemDto>>> Update(int id, [FromBody] UpdateItemDto dto)
        {
            try
            {
                var existingItem = await _repository.GetByIdAsync(id);
                if (existingItem == null)
                    return NotFound(ApiResponse<ItemDto>.ErrorResponse("Item not found"));

                _mapper.Map(dto, existingItem);
                await _repository.UpdateAsync(existingItem);

                var resultDto = _mapper.Map<ItemDto>(existingItem);
                return Ok(ApiResponse<ItemDto>.SuccessResponse(resultDto, "Item updated successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<ItemDto>.ErrorResponse(ex.Message));
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> Delete(int id)
        {
            try
            {
                var item = await _repository.GetByIdAsync(id);
                if (item == null)
                    return NotFound(ApiResponse<bool>.ErrorResponse("Item not found"));

                await _repository.DeleteAsync(item);
                return Ok(ApiResponse<bool>.SuccessResponse(true, "Item deleted successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<bool>.ErrorResponse(ex.Message));
            }
        }
    }

    // ========== Purchase Controller ==========
    [ApiController]
    [Route("api/[controller]")]
    public class PurchaseController : ControllerBase
    {
        private readonly IPurchaseRepository _repository;
        private readonly IItemRepository _itemRepository;
        private readonly IMapper _mapper;

        public PurchaseController(IPurchaseRepository repository, IItemRepository itemRepository, IMapper mapper)
        {
            _repository = repository;
            _itemRepository = itemRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<PurchaseDto>>>> GetAll()
        {
            try
            {
                var purchases = await _repository.GetPurchasesWithSupplierAsync();
                var dtos = _mapper.Map<IEnumerable<PurchaseDto>>(purchases);
                return Ok(ApiResponse<IEnumerable<PurchaseDto>>.SuccessResponse(dtos));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<IEnumerable<PurchaseDto>>.ErrorResponse(ex.Message));
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<PurchaseDto>>> GetById(int id)
        {
            try
            {
                var purchase = await _repository.GetPurchaseWithSupplierAsync(id);
                if (purchase == null)
                    return NotFound(ApiResponse<PurchaseDto>.ErrorResponse("Purchase not found"));

                var dto = _mapper.Map<PurchaseDto>(purchase);
                return Ok(ApiResponse<PurchaseDto>.SuccessResponse(dto));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<PurchaseDto>.ErrorResponse(ex.Message));
            }
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<PurchaseDto>>> Create([FromBody] CreatePurchaseDto dto)
        {
            try
            {
                var purchase = _mapper.Map<Purchase>(dto);
                await _repository.AddAsync(purchase);

                // Update primary item cost price and stock
                var primaryItem = await _itemRepository.GetPrimaryItemAsync();
                if (primaryItem != null)
                {
                    primaryItem.CostPrice = dto.Price;
                    primaryItem.AvailableStock += dto.Qty;
                    await _itemRepository.UpdateAsync(primaryItem);
                }

                var createdPurchase = await _repository.GetPurchaseWithSupplierAsync(purchase.PurchaseId);
                var resultDto = _mapper.Map<PurchaseDto>(createdPurchase);
                return CreatedAtAction(nameof(GetById), new { id = purchase.PurchaseId },
                    ApiResponse<PurchaseDto>.SuccessResponse(resultDto, "Purchase created successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<PurchaseDto>.ErrorResponse(ex.Message));
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<PurchaseDto>>> Update(int id, [FromBody] UpdatePurchaseDto dto)
        {
            try
            {
                var existingPurchase = await _repository.GetByIdAsync(id);
                if (existingPurchase == null)
                    return NotFound(ApiResponse<PurchaseDto>.ErrorResponse("Purchase not found"));

                var oldQty = existingPurchase.Qty;
                _mapper.Map(dto, existingPurchase);
                await _repository.UpdateAsync(existingPurchase);

                // Update primary item stock and price
                var primaryItem = await _itemRepository.GetPrimaryItemAsync();
                if (primaryItem != null)
                {
                    primaryItem.CostPrice = dto.Price;
                    primaryItem.AvailableStock = primaryItem.AvailableStock + dto.Qty - oldQty;
                    await _itemRepository.UpdateAsync(primaryItem);
                }

                var updatedPurchase = await _repository.GetPurchaseWithSupplierAsync(id);
                var resultDto = _mapper.Map<PurchaseDto>(updatedPurchase);
                return Ok(ApiResponse<PurchaseDto>.SuccessResponse(resultDto, "Purchase updated successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<PurchaseDto>.ErrorResponse(ex.Message));
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> Delete(int id)
        {
            try
            {
                var purchase = await _repository.GetByIdAsync(id);
                if (purchase == null)
                    return NotFound(ApiResponse<bool>.ErrorResponse("Purchase not found"));

                // Restore stock to primary item
                var primaryItem = await _itemRepository.GetPrimaryItemAsync();
                if (primaryItem != null)
                {
                    primaryItem.AvailableStock -= purchase.Qty;
                    await _itemRepository.UpdateAsync(primaryItem);
                }

                await _repository.DeleteAsync(purchase);
                return Ok(ApiResponse<bool>.SuccessResponse(true, "Purchase deleted successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<bool>.ErrorResponse(ex.Message));
            }
        }
    }

     // ========== Invoice Controller ==========
    [ApiController]
    [Route("api/[controller]")]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IItemRepository _itemRepository;
        private readonly IGenericRepository<InvoiceItem> _invoiceItemRepository;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public InvoiceController(
            IInvoiceRepository invoiceRepository,
            IItemRepository itemRepository,
            IGenericRepository<InvoiceItem> invoiceItemRepository,
            ApplicationDbContext context,
            IMapper mapper)
        {
            _invoiceRepository = invoiceRepository;
            _itemRepository = itemRepository;
            _invoiceItemRepository = invoiceItemRepository;
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<InvoiceDto>>>> GetAll()
        {
            try
            {
                var invoices = await _invoiceRepository.GetInvoicesWithDetailsAsync();
                var dtos = _mapper.Map<IEnumerable<InvoiceDto>>(invoices);
                return Ok(ApiResponse<IEnumerable<InvoiceDto>>.SuccessResponse(dtos));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<IEnumerable<InvoiceDto>>.ErrorResponse(ex.Message));
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<InvoiceDto>>> GetById(string id)
        {
            try
            {
                var invoice = await _invoiceRepository.GetInvoiceWithDetailsAsync(id);
                if (invoice == null)
                    return NotFound(ApiResponse<InvoiceDto>.ErrorResponse("Invoice not found"));

                var dto = _mapper.Map<InvoiceDto>(invoice);
                return Ok(ApiResponse<InvoiceDto>.SuccessResponse(dto));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<InvoiceDto>.ErrorResponse(ex.Message));
            }
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<InvoiceDto>>> Create([FromBody] CreateInvoiceDto dto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Get primary item for cost price
                var primaryItem = await _itemRepository.GetPrimaryItemAsync();
                if (primaryItem == null)
                    return BadRequest(ApiResponse<InvoiceDto>.ErrorResponse("Primary item not found"));

                // Generate invoice ID if not provided
                string invoiceId = dto.Id ?? $"INV-{DateTime.Now:yyyyMMddHHmmss}";

                // Calculate totals
                decimal totalQty = 0;
                decimal totalKg = 0;
                decimal totalAmount = 0;

                // Filter items with quantity > 0
                var validItems = dto.Items.Where(i => i.Qty > 0).ToList();

                foreach (var item in validItems)
                {
                    totalQty += item.Qty;
                    totalKg += item.TotalKg;
                    totalAmount += item.TotalKg * item.Price;
                }

                decimal grandTotal = totalAmount + dto.OtherCharges;

                // Create invoice
                var invoice = new Invoice
                {
                    Id = invoiceId,
                    Date = dto.Date,
                    CustomerId = dto.CustomerId,
                    GatePassNo = dto.GatePassNo,
                    TotalQty = (int)totalQty,
                    TotalKg = totalKg,
                    TotalAmount = totalAmount,
                    OtherCharges = dto.OtherCharges,
                    GrandTotal = grandTotal,
                    CostPrice = primaryItem.CostPrice
                };

                await _invoiceRepository.AddAsync(invoice);

                // Create invoice items (only for items with qty > 0)
                foreach (var itemDto in validItems)
                {
                    var invoiceItem = new InvoiceItem
                    {
                        InvoiceId = invoiceId,
                        ItemId = itemDto.ItemId,
                        Qty = itemDto.Qty,
                        TotalKg = itemDto.TotalKg,
                        Price = itemDto.Price
                    };
                    await _invoiceItemRepository.AddAsync(invoiceItem);
                }

                // Update primary item stock
                primaryItem.AvailableStock -= totalKg;
                await _itemRepository.UpdateAsync(primaryItem);

                await transaction.CommitAsync();

                var createdInvoice = await _invoiceRepository.GetInvoiceWithDetailsAsync(invoiceId);
                var resultDto = _mapper.Map<InvoiceDto>(createdInvoice);
                return CreatedAtAction(nameof(GetById), new { id = invoiceId },
                    ApiResponse<InvoiceDto>.SuccessResponse(resultDto, "Invoice created successfully"));
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, ApiResponse<InvoiceDto>.ErrorResponse(ex.Message));
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<InvoiceDto>>> Update(string id, [FromBody] UpdateInvoiceDto dto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var existingInvoice = await _invoiceRepository.GetInvoiceWithDetailsAsync(id);
                if (existingInvoice == null)
                    return NotFound(ApiResponse<InvoiceDto>.ErrorResponse("Invoice not found"));

                var oldTotalKg = existingInvoice.TotalKg;

                // Calculate new totals
                decimal totalQty = 0;
                decimal totalKg = 0;
                decimal totalAmount = 0;

                var validItems = dto.Items.Where(i => i.Qty > 0).ToList();

                foreach (var item in validItems)
                {
                    totalQty += item.Qty;
                    totalKg += item.TotalKg;
                    totalAmount += item.TotalKg * item.Price;
                }

                decimal grandTotal = totalAmount + dto.OtherCharges;

                // Update invoice
                existingInvoice.Date = dto.Date;
                existingInvoice.CustomerId = dto.CustomerId;
                existingInvoice.GatePassNo = dto.GatePassNo;
                existingInvoice.TotalQty = (int)totalQty;
                existingInvoice.TotalKg = totalKg;
                existingInvoice.TotalAmount = totalAmount;
                existingInvoice.OtherCharges = dto.OtherCharges;
                existingInvoice.GrandTotal = grandTotal;

                await _invoiceRepository.UpdateAsync(existingInvoice);

                // Delete existing items and recreate
                await _invoiceItemRepository.DeleteRangeAsync(existingInvoice.InvoiceItems);

                foreach (var itemDto in validItems)
                {
                    var invoiceItem = new InvoiceItem
                    {
                        InvoiceId = id,
                        ItemId = itemDto.ItemId,
                        Qty = itemDto.Qty,
                        TotalKg = itemDto.TotalKg,
                        Price = itemDto.Price
                    };
                    await _invoiceItemRepository.AddAsync(invoiceItem);
                }

                // Update primary item stock
                var primaryItem = await _itemRepository.GetPrimaryItemAsync();
                if (primaryItem != null)
                {
                    decimal kgDifference = totalKg - oldTotalKg;
                    primaryItem.AvailableStock -= kgDifference;
                    await _itemRepository.UpdateAsync(primaryItem);
                }

                await transaction.CommitAsync();

                var updatedInvoice = await _invoiceRepository.GetInvoiceWithDetailsAsync(id);
                var resultDto = _mapper.Map<InvoiceDto>(updatedInvoice);
                return Ok(ApiResponse<InvoiceDto>.SuccessResponse(resultDto, "Invoice updated successfully"));
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, ApiResponse<InvoiceDto>.ErrorResponse(ex.Message));
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> Delete(string id)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var invoice = await _invoiceRepository.GetInvoiceWithDetailsAsync(id);
                if (invoice == null)
                    return NotFound(ApiResponse<bool>.ErrorResponse("Invoice not found"));

                // Restore stock to primary item
                var primaryItem = await _itemRepository.GetPrimaryItemAsync();
                if (primaryItem != null)
                {
                    primaryItem.AvailableStock += invoice.TotalKg;
                    await _itemRepository.UpdateAsync(primaryItem);
                }

                await _invoiceRepository.DeleteAsync(invoice);
                await transaction.CommitAsync();

                return Ok(ApiResponse<bool>.SuccessResponse(true, "Invoice deleted successfully"));
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, ApiResponse<bool>.ErrorResponse(ex.Message));
            }
        }
    }

    // ========== PettyCash Controller ==========
    [ApiController]
    [Route("api/[controller]")]
    public class PettyCashController : ControllerBase
    {
        private readonly IGenericRepository<PettyCash> _repository;
        private readonly IPartyRepository _partyRepository;
        private readonly IMapper _mapper;

        public PettyCashController(
            IGenericRepository<PettyCash> repository,
            IPartyRepository partyRepository,
            IMapper mapper)
        {
            _repository = repository;
            _partyRepository = partyRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<PettyCashDto>>>> GetAll()
        {
            try
            {
                var pettyCashes = await _repository.GetAllAsync();
                var dtos = new List<PettyCashDto>();

                foreach (var pc in pettyCashes)
                {
                    var party = await _partyRepository.GetPartyWithTypeAsync(pc.PartyId);
                    var dto = _mapper.Map<PettyCashDto>(pc);
                    if (party != null)
                    {
                        dto.PartyName = party.PartyName;
                        dto.PartyTypeName = party.PartyType?.PartyTypeName ?? "";
                    }
                    dto.PaymentTypeName = pc.PaymentType == 1 ? "IN" : "OUT";
                    dtos.Add(dto);
                }

                return Ok(ApiResponse<IEnumerable<PettyCashDto>>.SuccessResponse(dtos));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<IEnumerable<PettyCashDto>>.ErrorResponse(ex.Message));
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<PettyCashDto>>> GetById(int id)
        {
            try
            {
                var pettyCash = await _repository.GetByIdAsync(id);
                if (pettyCash == null)
                    return NotFound(ApiResponse<PettyCashDto>.ErrorResponse("Petty cash entry not found"));

                var party = await _partyRepository.GetPartyWithTypeAsync(pettyCash.PartyId);
                var dto = _mapper.Map<PettyCashDto>(pettyCash);
                if (party != null)
                {
                    dto.PartyName = party.PartyName;
                    dto.PartyTypeName = party.PartyType?.PartyTypeName ?? "";
                }
                dto.PaymentTypeName = pettyCash.PaymentType == 1 ? "IN" : "OUT";

                return Ok(ApiResponse<PettyCashDto>.SuccessResponse(dto));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<PettyCashDto>.ErrorResponse(ex.Message));
            }
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<PettyCashDto>>> Create([FromBody] CreatePettyCashDto dto)
        {
            try
            {
                var pettyCash = _mapper.Map<PettyCash>(dto);
                await _repository.AddAsync(pettyCash);

                // Update party balance
                var party = await _partyRepository.GetByIdAsync(dto.PartyId);
                if (party != null)
                {
                    party.Balance += dto.Amount * dto.PaymentType;
                    await _partyRepository.UpdateAsync(party);
                }

                var result = await GetById(pettyCash.Id);
                return CreatedAtAction(nameof(GetById), new { id = pettyCash.Id },
                    ApiResponse<PettyCashDto>.SuccessResponse(result.Value?.Data, "Petty cash entry created successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<PettyCashDto>.ErrorResponse(ex.Message));
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<PettyCashDto>>> Update(int id, [FromBody] UpdatePettyCashDto dto)
        {
            try
            {
                var existingPettyCash = await _repository.GetByIdAsync(id);
                if (existingPettyCash == null)
                    return NotFound(ApiResponse<PettyCashDto>.ErrorResponse("Petty cash entry not found"));

                var oldPartyId = existingPettyCash.PartyId;
                var oldAmount = existingPettyCash.Amount;
                var oldPaymentType = existingPettyCash.PaymentType;

                // Reverse old balance effect
                var oldParty = await _partyRepository.GetByIdAsync(oldPartyId);
                if (oldParty != null)
                {
                    oldParty.Balance -= oldAmount * oldPaymentType;
                    await _partyRepository.UpdateAsync(oldParty);
                }

                _mapper.Map(dto, existingPettyCash);
                await _repository.UpdateAsync(existingPettyCash);

                // Apply new balance effect
                var newParty = await _partyRepository.GetByIdAsync(dto.PartyId);
                if (newParty != null)
                {
                    newParty.Balance += dto.Amount * dto.PaymentType;
                    await _partyRepository.UpdateAsync(newParty);
                }

                var result = await GetById(id);
                return Ok(ApiResponse<PettyCashDto>.SuccessResponse(result.Value?.Data, "Petty cash entry updated successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<PettyCashDto>.ErrorResponse(ex.Message));
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> Delete(int id)
        {
            try
            {
                var pettyCash = await _repository.GetByIdAsync(id);
                if (pettyCash == null)
                    return NotFound(ApiResponse<bool>.ErrorResponse("Petty cash entry not found"));

                // Reverse balance effect
                var party = await _partyRepository.GetByIdAsync(pettyCash.PartyId);
                if (party != null)
                {
                    party.Balance -= pettyCash.Amount * pettyCash.PaymentType;
                    await _partyRepository.UpdateAsync(party);
                }

                await _repository.DeleteAsync(pettyCash);
                return Ok(ApiResponse<bool>.SuccessResponse(true, "Petty cash entry deleted successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<bool>.ErrorResponse(ex.Message));
            }
        }
    }

    // ========== Assets Controller ==========
    [ApiController]
    [Route("api/[controller]")]
    public class AssetsController : ControllerBase
    {
        private readonly IGenericRepository<Assets> _repository;
        private readonly IMapper _mapper;

        public AssetsController(IGenericRepository<Assets> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<AssetsDto>>>> GetAll()
        {
            try
            {
                var assets = await _repository.GetAllAsync();
                var dtos = _mapper.Map<IEnumerable<AssetsDto>>(assets);
                return Ok(ApiResponse<IEnumerable<AssetsDto>>.SuccessResponse(dtos));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<IEnumerable<AssetsDto>>.ErrorResponse(ex.Message));
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<AssetsDto>>> GetById(int id)
        {
            try
            {
                var asset = await _repository.GetByIdAsync(id);
                if (asset == null)
                    return NotFound(ApiResponse<AssetsDto>.ErrorResponse("Asset not found"));

                var dto = _mapper.Map<AssetsDto>(asset);
                return Ok(ApiResponse<AssetsDto>.SuccessResponse(dto));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<AssetsDto>.ErrorResponse(ex.Message));
            }
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<AssetsDto>>> Create([FromBody] CreateAssetsDto dto)
        {
            try
            {
                var asset = _mapper.Map<Assets>(dto);
                await _repository.AddAsync(asset);

                var resultDto = _mapper.Map<AssetsDto>(asset);
                return CreatedAtAction(nameof(GetById), new { id = asset.AssetsId },
                    ApiResponse<AssetsDto>.SuccessResponse(resultDto, "Asset created successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<AssetsDto>.ErrorResponse(ex.Message));
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<AssetsDto>>> Update(int id, [FromBody] UpdateAssetsDto dto)
        {
            try
            {
                var existingAsset = await _repository.GetByIdAsync(id);
                if (existingAsset == null)
                    return NotFound(ApiResponse<AssetsDto>.ErrorResponse("Asset not found"));

                _mapper.Map(dto, existingAsset);
                await _repository.UpdateAsync(existingAsset);

                var resultDto = _mapper.Map<AssetsDto>(existingAsset);
                return Ok(ApiResponse<AssetsDto>.SuccessResponse(resultDto, "Asset updated successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<AssetsDto>.ErrorResponse(ex.Message));
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> Delete(int id)
        {
            try
            {
                var asset = await _repository.GetByIdAsync(id);
                if (asset == null)
                    return NotFound(ApiResponse<bool>.ErrorResponse("Asset not found"));

                await _repository.DeleteAsync(asset);
                return Ok(ApiResponse<bool>.SuccessResponse(true, "Asset deleted successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<bool>.ErrorResponse(ex.Message));
            }
        }
    }
}