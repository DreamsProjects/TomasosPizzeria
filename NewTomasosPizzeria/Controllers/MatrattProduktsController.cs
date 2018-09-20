using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewTomasosPizzeria.Models;
using Newtonsoft.Json;

namespace NewTomasosPizzeria.Controllers
{
    public class MatrattProduktsController : Controller
    {
        private readonly NyTomasosContext _context;

        private List<Matratt> FoodList;

        public MatrattProduktsController(NyTomasosContext context)
        {
            _context = context;
        }

        public async Task<ActionResult> Welcome()
        {
            var dish = _context.Matratt.ToList();
            var ingredients = _context.Produkt.ToList();
            var con = _context.MatrattProdukt.ToList();
            var type = _context.MatrattTyp.ToList();

            var model = new MenuModel
            {
                Dishes = dish,
                Ingredients = ingredients,
                DishConnection = con,
                Types = type
            };

            return View(model);
        }

        public async Task<ActionResult> Sent(Kund kund) 
        {
            if (HttpContext.Session.GetString("KundId") != null)
            {
                var kundId = HttpContext.Session.GetString("KundId");

                var convert = Convert.ToInt32(kundId);

                var value = (HttpContext.Session.GetString("Varukorg"));
                FoodList = JsonConvert.DeserializeObject<List<Matratt>>(value);

                var date = DateTime.Now;

                var order = new Bestallning
                {
                    BestallningDatum = date,
                    Totalbelopp = FoodList.Sum(x => x.Pris),
                    Levererad = false,
                    KundId = Convert.ToInt32(convert)
                };
                _context.Bestallning.Add(order);

               var listSum = FoodList;

               var tempList = FoodList.OrderBy(x => x.MatrattId);

                foreach (var sum in tempList.Select(x => x.MatrattId).Distinct())
                {

                    var doneOrder = new BestallningMatratt
                    {
                        BestallningId = order.BestallningId,
                        Antal = listSum.Where(x => x.MatrattId == sum).Count(),
                        MatrattId = sum
                    };

                    _context.BestallningMatratt.Add(doneOrder);
                }

                await _context.SaveChangesAsync();

                var dish = _context.Bestallning.Where(x => x.BestallningId == order.BestallningId).ToList();
                var customer = _context.Kund.Where(x => x.KundId == convert).ToList();

                var display = new MenuModel
                {
                    Orders = dish,
                    CustomerList = customer,
                    Dishes = FoodList,
                };

                return View(display); // await tomasosContext.ToListAsync()
            }

            else
            {
                return RedirectToAction("Login", "Kunds");
            }
        }

        public async Task<ActionResult> AddToCart(int id)
        {
            var foodOrder = _context.Matratt.SingleOrDefault(x => x.MatrattId == id);
            // List<Matratt> FoodList;

            if (HttpContext.Session.GetString("Varukorg") == null)
            {
                FoodList = new List<Matratt>();
            }

            else //Hämta lista från sessionen
            {
                var value = (HttpContext.Session.GetString("Varukorg"));
                FoodList = JsonConvert.DeserializeObject<List<Matratt>>(value);
            }

            FoodList.Add(foodOrder); //Lägger tillbaka listan i sessionen

            var tempo = JsonConvert.SerializeObject(FoodList);
            HttpContext.Session.SetString("Varukorg", tempo);

            return RedirectToAction("Welcome");
        }

        public async Task<ActionResult> RemoveFromCart(int id)
        {
            var value = (HttpContext.Session.GetString("Varukorg"));
            //behöver ta in listan från sessionen för att få in värdena innan jag tar bort det utvalda elemetet
            FoodList = JsonConvert.DeserializeObject<List<Matratt>>(value);

            var item = FoodList.FirstOrDefault(x => x.MatrattId == id);
            FoodList.Remove(item);

            var tempo = JsonConvert.SerializeObject(FoodList);
            HttpContext.Session.SetString("Varukorg", tempo);

            return RedirectToAction("CheckOut");
        }

        public IActionResult CheckOut()
        {

            var value = (HttpContext.Session.GetString("Varukorg"));

            if (value == null)
            {
                return RedirectToAction(nameof(Welcome)); //Om man försöker öppna en tom varukorg, annars blir det Db error på hemsidan.
            }

            var model = JsonConvert.DeserializeObject<List<Matratt>>(value);


            return View(model);
        }
    }
}