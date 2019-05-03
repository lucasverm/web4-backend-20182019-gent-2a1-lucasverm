﻿using BijenkastApi.DTOs;
using BijenkastApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace BijenkastApi.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class BijenkastenController : ControllerBase
    {
        private readonly IBijenkastRepository _bijenkastRepository;
        private readonly IImkerRepository _imkerRepository;

        public BijenkastenController(IBijenkastRepository context, IImkerRepository imkerRepository)
        {
            _bijenkastRepository = context;
            _imkerRepository = imkerRepository;
        }

        ///<summary>
        /// Geeft alle bijenkasten terug
        /// </summary>
        ///<returns>De Bijenkasten</returns>
        [HttpGet]
        public IEnumerable<Bijenkast> GetBijenkasten()
        {
            Imker imker = _imkerRepository.GetBy(User.Identity.Name);
            if (imker == null) { return Unauthorized(); };
            return _bijenkastRepository.GetAll(imker.ImkerId).OrderBy(r => r.naam);
        }

        ///<summary>
        /// Geeft 1 specifieke bijenkast terug dmv een id
        /// </summary>
        /// ///<param name="imkerId">het id van de imker</param>
        ///<param name="kastId">het id van de bijenkast</param>
        ///<returns>De bijenkast met opgegeven id</returns>
        [HttpGet("{imkerId}/{kastId}")]
        public ActionResult<Bijenkast> GetBijenkast(int imkerId, int kastId)
        {
            Bijenkast bijenkast = _bijenkastRepository.GetBy(kastId);
            if (bijenkast.imkerId != imkerId) return Unauthorized();
            if (bijenkast == null) return NotFound();
            return bijenkast;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Bijenkast> PostBijenkast(BijenkastDTO bijenkast)
        {
            Bijenkast aanTeMakenBijenkast = new Bijenkast(bijenkast.naam,
            bijenkast.type, bijenkast.aantalhoningkamers, bijenkast.aantalbroedkamers, bijenkast.aantalramenperkamer, bijenkast.bijenras,
            bijenkast.moergeboortedag, bijenkast.moergeboortemaand, bijenkast.moergeboortejaar,
            bijenkast.moergemerkt, bijenkast.moergeknipt,
 bijenkast.moerbevrucht,
 bijenkast.aanmaakdag, bijenkast.aanmaakmaand, bijenkast.aanmaakjaar

            );
            Imker imker = _imkerRepository.GetBy(bijenkast.imkerId);
            imker.bijenkasten.Add(aanTeMakenBijenkast);
            _bijenkastRepository.Add(aanTeMakenBijenkast);
            _imkerRepository.Update(imker);
            _bijenkastRepository.SaveChanges();
            _imkerRepository.SaveChanges();
            return aanTeMakenBijenkast;
        }

        // PUT: api/Bijenkasten/1
        [HttpPut("{imkerId}/{kastId}")]
        public ActionResult<Bijenkast> PutBijenkast(int imkerId, int kastId, BijenkastDTO bijenkast)
        {
            if (bijenkast.imkerId != imkerId) return Unauthorized();
            Bijenkast upTeDatenKast = _bijenkastRepository.GetBy(kastId);

            upTeDatenKast.naam = bijenkast.naam;
            upTeDatenKast.type = bijenkast.type;
            upTeDatenKast.aantalhoningkamers = bijenkast.aantalhoningkamers;
            upTeDatenKast.aantalbroedkamers = bijenkast.aantalbroedkamers;
            upTeDatenKast.aantalramenperkamer = bijenkast.aantalramenperkamer;
            upTeDatenKast.bijenras = bijenkast.bijenras;
            upTeDatenKast.moergeboortedag = bijenkast.moergeboortedag;
            upTeDatenKast.moergeboortejaar = bijenkast.moergeboortejaar;
            upTeDatenKast.moergeboortemaand = bijenkast.moergeboortemaand;
            upTeDatenKast.moergemerkt = bijenkast.moergemerkt;
            upTeDatenKast.moergeknipt = bijenkast.moergeknipt;
            upTeDatenKast.moerbevrucht = bijenkast.moerbevrucht;
            upTeDatenKast.aanmaakdag = bijenkast.aanmaakdag;
            upTeDatenKast.aanmaakmaand = bijenkast.aanmaakmaand;
            upTeDatenKast.aanmaakjaar = bijenkast.aanmaakjaar;

            _bijenkastRepository.Update(upTeDatenKast);
            _bijenkastRepository.SaveChanges();
            return upTeDatenKast;
        }

        // DELETE: api/Bijenkasten/5
        [HttpDelete("{id}")]
        public ActionResult<Bijenkast> DeleteBijenkast(int id)
        {
            Bijenkast bijenkast = _bijenkastRepository.GetBy(id);
            if (bijenkast == null)
            {
                return NotFound();
            }
            _bijenkastRepository.Delete(bijenkast);
            _bijenkastRepository.SaveChanges();
            return bijenkast;
        }
    }
}