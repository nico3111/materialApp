using ClosedXML.Excel;
using DocumentFormat.OpenXml.Office.CustomUI;
using MaterialData.models;
using MaterialData.models.material;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Packaging;
using MaterialData.repository;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace MaterialData.repositories
{
    public class ExportRepository
    {
        SearchRepository search;
        DcvEntities entities;
        public string fileName = "Material.xlsx";

        public ExportRepository(DcvEntities entities)
        {
            this.entities = entities;
            search = new SearchRepository(entities);
        }

        public XLWorkbook Export(search materials)
        {
            Dictionary<string, List<Material>> materialDict = search.GetResult(materials);

            var workbook = new XLWorkbook();

            foreach (var item in materialDict)
            {
                var sheetName = item.Key;
                switch (sheetName)
                {
                    case "notebook":
                        sheetName = "Notebooks";
                        break;
                    case "display":
                        sheetName = "Bildschirme";
                        break;
                    case "book":
                        sheetName = "Bücher";
                        break;
                    case "equipment":
                        sheetName = "Zubehör";
                        break;
                    case "furniture":
                        sheetName = "Mobiliar";
                        break;
                }

                if (item.Value.Count > 0)
                {
                    var worksheet = workbook.Worksheets.Add(sheetName);

                    if (sheetName.Equals("Notebooks"))
                    {
                        worksheet.Cell(1, 2).Value = "Marke";
                        worksheet.Cell(1, 3).Value = "Modell";
                        worksheet.Cell(1, 4).Value = "Seriennummer";
                        worksheet.Cell(1, 5).Value = "Vorname";
                        worksheet.Cell(1, 6).Value = "Nachname";
                        worksheet.Cell(1, 7).Value = "Räumlichkeit";
                    }

                    if (sheetName.Equals("Bildschirme"))
                    {
                        worksheet.Cell(1, 2).Value = "Marke";
                        worksheet.Cell(1, 3).Value = "Modell";
                        worksheet.Cell(1, 4).Value = "Seriennummer";
                        worksheet.Cell(1, 5).Value = "Räumlichkeit";
                        worksheet.Cell(1, 6).Value = "Anzahl";
                    }

                    if (sheetName.Equals("Bücher"))
                    {
                        worksheet.Cell(1, 2).Value = "Titel";
                        worksheet.Cell(1, 3).Value = "ISBN";
                        worksheet.Cell(1, 4).Value = "Vorname";
                        worksheet.Cell(1, 5).Value = "Nachname";
                        worksheet.Cell(1, 6).Value = "Räumlichkeit";
                        worksheet.Cell(1, 7).Value = "Anzahl";
                    }

                    if (sheetName.Equals("Zubehör"))
                    {
                        worksheet.Cell(1, 2).Value = "Art";
                        worksheet.Cell(1, 3).Value = "Marke";
                        worksheet.Cell(1, 4).Value = "Modell";
                        worksheet.Cell(1, 5).Value = "Vorname";
                        worksheet.Cell(1, 6).Value = "Nachname";
                        worksheet.Cell(1, 7).Value = "Räumlichkeit";
                        worksheet.Cell(1, 8).Value = "Anzahl";
                    }

                    if (sheetName.Equals("Mobiliar"))
                    {
                        worksheet.Cell(1, 2).Value = "Art";
                        worksheet.Cell(1, 3).Value = "Räumlichkeit";
                        worksheet.Cell(1, 4).Value = "Anzahl";
                    }

                    for (int i = 0; i < item.Value.Count; i++)
                    {
                        var item1 = item.Value[i];

                        worksheet.Cell(1, 1).Value = "ID";
                        worksheet.Cell(i + 2, 1).Value = item1.id;

                        if (item1.GetType() == typeof(notebook))
                        {
                            NotebookRepository Repo = new NotebookRepository(entities);
                            Repo.GetRelation();
                            var notebook = (notebook)item1;

                            worksheet.Cell(i + 2, 2).Value = notebook.make;
                            worksheet.Cell(i + 2, 3).Value = notebook.model;
                            worksheet.Cell(i + 2, 4).Value = notebook.serial_number;                            
                            if (notebook.person != null)
                            {
                                worksheet.Cell(i + 2, 5).Value = notebook.person.name1;
                                worksheet.Cell(i + 2, 6).Value = notebook.person.name2;
                            }
                            if (notebook.classroom != null)
                                worksheet.Cell(i + 2, 7).Value = notebook.classroom.room;

                            worksheet.Columns().AdjustToContents();
                            worksheet.Rows().AdjustToContents();
                        }

                        else if (item1.GetType() == typeof(display))
                        {
                            DisplayRepository Repo = new DisplayRepository(entities);
                            Repo.GetRelation();

                            var display = (display)item1;
                            worksheet.Cell(i + 2, 2).Value = display.make;
                            worksheet.Cell(i + 2, 3).Value = display.model;
                            if (display.serial_number != null)
                                worksheet.Cell(i + 2, 4).Value = display.serial_number;

                            if (display.classroom != null)
                                worksheet.Cell(i + 2, 5).Value = display.classroom.room;

                            if (display.quantity != null)
                                worksheet.Cell(i + 2, 6).Value = display.quantity;

                            worksheet.Columns().AdjustToContents();
                            worksheet.Rows().AdjustToContents();
                        }

                        else if (item1.GetType() == typeof(book))
                        {
                            BookRepository Repo = new BookRepository(entities);
                            Repo.GetRelation();

                            var book = (book)item1;
                            worksheet.Cell(i + 2, 2).Value = book.title;
                            worksheet.Cell(i + 2, 3).Value = book.isbn;                            

                            if (book.person != null)
                            {
                                worksheet.Cell(i + 2, 4).Value = book.person.name1;
                                worksheet.Cell(i + 2, 5).Value = book.person.name2;
                            }
                            if (book.classroom != null)
                                worksheet.Cell(i + 2, 6).Value = book.classroom.room;
                            if (book.quantity != null)
                                worksheet.Cell(i + 2, 7).Value = book.quantity;

                            worksheet.Columns().AdjustToContents();
                            worksheet.Rows().AdjustToContents();
                        }

                        else if (item1.GetType() == typeof(equipment))
                        {
                            EquipmentRepository Repo = new EquipmentRepository(entities);
                            Repo.GetRelation();
                            var equipment = (equipment)item1;

                            worksheet.Cell(i + 2, 2).Value = equipment.type;

                            if (equipment.make != null)
                                worksheet.Cell(i + 2, 3).Value = equipment.make;


                            if (equipment.model != null)
                                worksheet.Cell(i + 2, 4).Value = equipment.model;                            

                            if (equipment.person != null)
                            {
                                worksheet.Cell(i + 2, 5).Value = equipment.person.name1;
                                worksheet.Cell(i + 2, 6).Value = equipment.person.name2;
                            }
                            if (equipment.classroom != null)
                                worksheet.Cell(i + 2, 7).Value = equipment.classroom.room;

                            if (equipment.quantity != null)
                                worksheet.Cell(i + 2, 8).Value = equipment.quantity;

                            worksheet.Columns().AdjustToContents();
                            worksheet.Rows().AdjustToContents();
                        }

                        else if (item1.GetType() == typeof(furniture))
                        {
                            FurnitureRepository Repo = new FurnitureRepository(entities);
                            Repo.GetRelation();

                            var furniture = (furniture)item1;

                            worksheet.Cell(i + 2, 2).Value = furniture.type;

                            if (furniture.classroom != null)
                                worksheet.Cell(i + 2, 3).Value = furniture.classroom.room;


                            if (furniture.quantity != null)
                                worksheet.Cell(i + 2, 4).Value = furniture.quantity;

                            worksheet.Columns().AdjustToContents();
                            worksheet.Rows().AdjustToContents();
                        }
                    }
                }
            }
            return workbook;
        }
    }
}