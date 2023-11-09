using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;
public class CustomerController : BaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CustomerController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<CustomerDto>>> Get()
    {
        var con = await _unitOfWork.Customers.GetAllAsync();

        return _mapper.Map<List<CustomerDto>>(con);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CustomerDto>> Get(int id){
        var con = await _unitOfWork.Customers.GetByIdAsync(id);
        if (con == null){
            return NotFound();
        }
        return _mapper.Map<CustomerDto>(con);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CustomerDto>> Post(CustomerDto CustomerDto){
        var con = _mapper.Map<Customer>(CustomerDto);
        _unitOfWork.Customers.Add(con);
        await _unitOfWork.SaveAsync();
        if(con == null){
            return BadRequest();
        }
        CustomerDto.Id = con.Id;
        return CreatedAtAction(nameof(Post), new {id = CustomerDto.Id}, CustomerDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CustomerDto>> Put(int id, [FromBody]CustomerDto CustomerDto){
        if(CustomerDto.Id == 0){
            CustomerDto.Id = id;
        }

        if(CustomerDto.Id != id){
            return BadRequest();
        }

        if(CustomerDto == null){
            return NotFound();
        }
        var con = _mapper.Map<Customer>(CustomerDto);
        _unitOfWork.Customers.Update(con);
        await _unitOfWork.SaveAsync();
        return CustomerDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id){
        var con = await _unitOfWork.Customers.GetByIdAsync(id);
        if(con == null){
            return NotFound();
        }
        _unitOfWork.Customers.Remove(con);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}
