using System;
using GMXHelper;
using System.Collections.Generic;

namespace GMX.ViewModels
{
    public class Emails
    {
        public static DocumentPDF docPDF { get; set; }
        public static List<Section> Sections { get; set; }

        public static void GetSlipCotizacion(VMCotizar vm)
        {
            //GMXITServiceClient GMXITService = new GMXITServiceClient();
            //bl_DocumentacionGral obj_doc_gral = new bl_DocumentacionGral();
            //byte[] temp = obj_doc_gral.DocumentacionGral("Watermark.jpg", false);
            docPDF = new DocumentPDF();
            Sections = new List<Section>();

            //docPDF.Watermark = temp;
            string Header = "<center><table><tr><td><br/><h2>ESPECIFICACIÓN QUE SE ADHIERE Y FORMA PARTE INTEGRANTE DE LA PÓLIZA</h2><h2></h2><br/><br/></td></tr></table></center><br/><br/>";
            docPDF.StaticText = Header;
            docPDF.StaticCSS = "h2{ font-family: 'Segoe UI'; font-size: small; text-align: center; color: #9c9c9c; } h3{ font-family: 'Segoe UI'; font-size: xx-small; text-align: center; color: #9c9c9c; } table{border: 0px solid #d0d0d0;  margin: auto; width:100 %; }";
            docPDF.StaticXPosition = 110;
            docPDF.StaticYPosition = 700;

            //Pie de Pagina
            docPDF.PageFoot = "PVL – GMX SEGUROS";
            docPDF.PageFootx = 150;

            //Pie de Pagina Numeros
            docPDF.PageNum = true;
            docPDF.mTop = 140;
            docPDF.mBottom = 70;
            docPDF.mRight = 58;
            docPDF.mLeft = 58;

            string Today = String.Format("{0:d}", DateTime.Today);
            string SumaAseg = vm.SumaAseg;
            string pNeta = vm.PrimaNeta.ToString("c");
            string recargos = 0.ToString("c");
            string derechos = vm.Derechos.ToString("c");
            string sTotal = vm.SubTotal.ToString("c");
            string iva = vm.Iva.ToString("c");
            string pTotal = vm.PrimaTotal.ToString("c");
            #region Sections Fecha Expedicion
            Sections.Add(new Section
            {
                Text = "<p>Fecha de expedición: " + Today + "</p><br/>"
               ,
                CSSFont = "p{ font-size: 12; font-family : Arial; text-align: left;}"
            });

            Sections.Add(new Section
            {
                Text = "<p>La presente Cotización es sólo una propuesta por lo que no implica la aceptación del riesgo por parte de Grupo Mexicano de Seguros S.A. de C.V., cualquier cambio a la información proporcionada podrá modificar la Cotización o en su caso anularla.</p>"
               ,
                CSSFont = "p{ font-size: 12; font-family : Arial; text-align: justify;}"
            });
            #endregion
            #region Sections Contenido
            Sections.Add(new Section
            {
                Text = "<table cellpadding='5' class='ctable'>" +
                                   "<tr class='ctr'> " +
                                       "<td class='ctd' style='width: 20%;'><p><b>Límite Máximo de Responsabilidad</b></p></td> " +
                                       "<td class='ctd' style='width: 60%;'><p>Límite Máximo de Responsabilidad Único y Máximo en el agregado anual incluyendo Gastos de Defensa Juridica: " + SumaAseg + "</p></td>" +
                                   "</tr>" +
                                   "<tr>" +
                                   "<td class='ctd' style='width: 20%;'><p><b>Coberturas</b></p></td> " +
                                   "<td class='ctd' style='width: 60%;'>" +
                                   "<p>" +
                                         "<ul style='list-style-type:Disc'> " +
                                         "<li><b>Responsabilidad Civil Profesional.</b></li>" +
                                         "<li><b>Responsabilidad Civil por el uso de objetos peligrosos (objetiva)</b></li>" +
                                         "<li><b>Suministros de medicamentos y materiales de curación</b></li>" +
                                         "<li><b>Responsabilidad Civil por sus inmuebles y Responsabilidad Civil por sus Actividades </b></li>" +
                                         "</ul>" +
                                   "</p>" +
                                   "</td>" +
                                   "</tr>" +
                                   "<tr class='ctr'>" +
                                       "<td class='ctd' style='width: 20%;'><p><b>Prima Total</b></p></td>" +
                                       "<td class='ctd' style='width: 60%;'>" +
                                           "<table> " +
                                               "<tr class='ctr'><td><p>Prima Neta:</p></td><td><p>" + pNeta + "</p></td></tr> " +
                                               "<tr class='ctr'><td><p>Recargos:</p></td><td><p>" + recargos + "</p></td></tr> " +
                                               "<tr class='ctr'><td><p>Derechos:</p></td><td><p>" + derechos + "</p></td></tr> " +
                                               "<tr class='ctr'><td><p>Subtotal:</p></td><td><p>" + sTotal + "</p></td></tr> " +
                                               "<tr class='ctr'><td><p>I.V.A:</p></td><td><p>" + iva + "</p></td></tr> " +
                                               "<tr class='ctr'><td><p>Prima Total:</p></td><td><p>" + pTotal + "</p></td></tr> " +
                                           "</table> " +
                                       "</td> " +
                                   "</tr>" +
                                    "<tr  class='ctr'>" +
                                        "<td class='ctd'style='width: 20%;'><p><b>Riesgo Asegurado</b></p></td> " +
                                        "<td class='ctd'style='width: 60%;'> " +
                                            "<p>Seguro de responsabilidad civil y de responsabilidad civil profesional para profesiones médicas y sus profesiones auxiliares y técnicas:</p><br/>" +
                                            "<p>GMX Seguros se obliga a pagar la indemnización que el Asegurado deba a sus pacientes o a terceros dañados a consecuencia de uno o más hechos realizados sin dolo, ya sea por culpa negligente o imperita (acciones u omisiones), que ocasione en el ejercicio de las profesiones médicas o de las profesiones técnicas o auxiliares de la medicina o por el uso de cosas peligrosas.</p><br/>" +
                                            "<p>También queda incluida dentro de esta última la responsabilidad por el uso de mecanismos, instrumentos, aparatos o substancias peligrosas por sí mismos y que dan lugar a responsabilidad civil, derivados de su actividad e inmueble.</p><br/>" +
                                            "<p>Estos aparatos pueden ser todos los usados para fines del diagnóstico y de la terapéutica, en cuanto estén reconocidos por la ciencia médica, y causen un daño previsto en esta póliza a terceras personas con motivo de la prestación de servicios para la salud.</p><br/>" +
                                            "<p>Para la cobertura de Responsabilidad Civil los daños comprenden: lesiones corporales, enfermedades, muerte; así como el deterioro o destrucción de bienes de terceras personas. Los perjuicios que resulten y el daño moral sólo se cubren cuando sean consecuencia directa e inmediata de los citados daños, causados a sus pacientes o a terceros dañados.</p>" +
                                        "</td> " +
                                    "</tr> " +
                                   "<tr class='ctr'> " +
                                        "<td class='ctd' style='width: 20%;'><p><b>Base de Indemnización</b></p></td> " +
                                        "<td class='ctd' style='width: 60%;' > " +
                                            "<p>Conforme a lo dispuesto en el Art. 145 bis de la ley sobre el Contrato de Seguro, el presente seguro cubre la indemnización que el Asegurado deba a un tercero, conforme a las condiciones pactadas en el presente contrato, por hechos ocurridos durante la vigencia de la póliza, siempre que la reclamación se formule por primera vez y por escrito al Asegurado o a GMX Seguros, en el curso de la vigencia de esta póliza o dentro del siguiente año a su terminación.</p>" +
                                        "</td> " +
                                   "</tr> " +
                                   "<tr class='ctr'> " +
                                        "<td class='ctd' style='width: 20%;'><p><b>Exclusiones Particulares</b></p></td> " +
                                        "<td class='ctd' style='width: 80%;'>" +
                                                                       "<p>" +
                                                                             "<ul style='list-style-type:Disc'> " +
                                                                             "<li><b>Acciones dolosas de parte del asegurado</b></li>" +
                                                                             "<li><b>Garantía de calidad del servicio</b></li>" +
                                                                             "<li><b>Incumplimiento de contratos</b></li>" +
                                                                             "<li><b>Multas, daños punitivos o ejemplares y/o venganza</b></li>" +
                                                                            "<li><b>Obligaciones derivadas de un contrato</b></li>" +
                                                                            "<li><b>R.C. Productos, incluyendo dentro de éstos los farmacéuticos y transgénicos </b></li>" +
                                                                            "<li><b>R.C. Patronal</b></li>" +
                                                                            "<li><b>R.C. Contractual</b></li>" +
                                                                             "</ul>" +
                                                                       "</p><br/>" +
                                                                       "<p><b>Demás que señalan las condiciones generales</b></p>" +
                                                    "</td>" +
                                    "</tr> " +
                                    "<tr class='ctr'> " +
                                    "<td class='ctd' style='width: 20%;'><p><b>Deducible general</b></p></td> " +
                                    "<td class='ctd' style='width: 80%;'>" +
                                                                       "<p>" +
                                                                             "<ul style='list-style-type:Disc'> " +
                                                                             "<li>Para daños a terceros en sus personas: Sin deducible.</li>" +
                                                                             "<li>Para daños a terceros en sus propiedades: Sin deducible.</li>" +
                                                                             "</ul>" +
                                                                       "</p>" +
                                     "</td>" +
                                    "</tr> " +
                                    "<tr class='ctr'> " +
                                        "<td class='ctd' style='width: 20%;'><p><b>Función de Defensa Jurídica </b></p></td> " +
                                        "<td class='ctd' style='width: 80%;'>" +
                                                                       "<p>Quedan a cargo de GMX Seguros y dentro del límite de responsabilidad asegurado en esta póliza el pago de los gastos de defensa legal del Asegurado.</p><br/>" +
                                                                       "<p>Dichos gastos incluyen la tramitación judicial y extrajudicial, por las vías civil, penal y administrativa, así como el análisis de las reclamaciones de terceros, aun cuando ellas sean infundadas, y las cauciones y primas de fianzas requeridas procesalmente.</p><br/>" +
                                                                       "<p>Los daños y los perjuicios se determinan conforme a la legislación vigente en los Estados Unidos Mexicanos.</p><br/>" +
                                        "</td>" +
                                    "</tr> " +
                                    "<tr class='ctr'> " +
                                        "<td class='ctd' style='width: 20%;'><p><b>Jurisdicción</b></p></td> " +
                                        "<td class='ctd' style='width: 80%;'>" +
                                                                       "<p>La responsabilidad civil materia del seguro se determina conforme a la legislación vigente en los Estados Unidos Mexicanos.</p><br/>" +
                                        "</td>" +
                                    "</tr> " +
                                    "<tr class='ctr'> " +
                                        "<td class='ctd' style='width: 20%;'><p><b>Condiciones Especiales </b></p></td> " +
                                        "<td class='ctd' style='width: 80%;'>" +
                                                                       "<p>Todos los términos y condiciones conforme al texto de Seguro de responsabilidad civil y de responsabilidad civil profesional para profesiones médicas y sus profesiones auxiliares y técnicas.</p><br/>" +
                                        "</td>" +
                                    "</tr> " +
                          "</table>"
                                   ,
                CSSFont = "table { font-family: Arial; font-size: 10px; }" +
                            ".ctable{  border-style: solid; border-width: 1px; border-color: #C0C0C0;  width: 100%; border-collapse: collapse; }" +
                            ".ctr{ border-style: solid;     border-width: 1px; border-color: #C0C0C0;  width: 100%; border-collapse: collapse; }" +
                          ".ctd{ border-style: solid;       border-width: 1px; border-color: #C0C0C0;  width: 100%; border-collapse: collapse; }"
            });
            #endregion
        }
    }
}
