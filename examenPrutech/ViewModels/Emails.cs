using System;
using GMXHelper;
using System.Collections.Generic;
using System.Globalization;
using GMX.Services;
using Newtonsoft.Json;
using GMX.Services.DTOs;

namespace GMX.ViewModels
{
    public class Emails
    {
        public static DocumentPDF docPDF { get; set; }
        public static List<Section> Sections { get; set; }


        public static string Reenvio()
        {
            string res = "<!DOCTYPE html><html><head><title></title><meta charset='utf-8' /></head><body></body></html><body><div style='font-family: Tw Cen MT;'><table cellspacing='0' cellpadding='40' align='center' style='font-family: Tw Cen MT; width: 80%; border-style: solid; border-width: 2px;  border-color: #1ba2a4; font-size: 11pt; color: #1ba2a4; margin-top: 2px; text-align:justify;'><tr><td style='width:40%; height:100%;' rowspan='3' align='center' valign='top'><p style='font-size: large; text-align: left; color: #1ba2a4; font-family: Tw Cen MT;'><br /><span style='font-size: xx-large; font-weight: bold; text-align: left; font-family: Tw Cen MT; color: #1ba2a4;'>GMX Seguros</span><br />Juntos el riesgo es menor<sup style='font-size:x-small;'>MR</sup></p><br><p style='font-size: medium; color: #1ba2a4; font-family: Tw Cen MT;'>A su solicitud reenviamos los documentos</p></td><td style='width:5%; height:100%;' align='center' valign='top' rowspan='3' bgcolor='#1ba2a4'><img src='http://desa.gmx.com.mx/wsMAIL/MailsComponents/PVLMED/BotonMail.png' width='132' /></td></tr></table></div></body>";
            return res;
        }

        #region Tradicional
        public static void SlipTradicional(string numpoliza, bool rc, string nombrecliente, string desc, string esp, string cedprof, string cedesp, string diplo, string sumaaseg)
        {
            docPDF = new DocumentPDF();
            Sections = new List<Section>();

            string Header = "<center><table><tr><td><br/><h2>ESPECIFICACIÓN QUE SE ADHIERE Y FORMA PARTE INTEGRANTE DE LA PÓLIZA</h2><h2>" + numpoliza + "</h2><br/><br/></td></tr></table></center><br/><br/>";
            docPDF.StaticText = Header;
            docPDF.StaticCSS = "h2{ font-family: 'Segoe UI'; font-size: small; text-align: center; color: #9c9c9c; } h3{ font-family: 'Segoe UI'; font-size: xx-small; text-align: center; color: #9c9c9c; } table{border: 0px solid #d0d0d0;  margin: auto; width:100 %; }";

            docPDF.PVLID4CondGrales = "PVLM3D";
            docPDF.StaticXPosition = 110; // Izq/Der
            docPDF.StaticYPosition = 700; // Arriba/abajo

            //Pie de Pagina
            docPDF.PageFoot = "PVL – GMX SEGUROS";
            docPDF.PageFootx = 150;

            //Pie de Pagina Numeros
            docPDF.PageNum = true;
            docPDF.mTop = 140;
            docPDF.mBottom = 70;
            docPDF.mRight = 58;
            docPDF.mLeft = 58;

            string cober_rcArrend = rc ? "<li><b>Responsabilidad Civil Arrendatario</b></li>" : string.Empty;

            
            Sections.Add(new Section
            {
                Text = "<table cellpadding='5' class='ctable'>" +
                                   "<tr class='ctr'> " +
                                       "<td class='ctd' style='width: 20%;'><p><b>Asegurado:</b></p></td> " +
                    "<td class='ctd' style='width: 60%;'><p>" + nombrecliente + "</p></td>" +
                                   "</tr>" +
                                   "<tr class='ctr'> " +
                                       "<td class='ctd' style='width: 20%;'><p><b>Especificación:</b></p></td> " +
                    "<td class='ctd' style='width: 60%;'><p>" + desc + "</p></td>" +
                                   "</tr>" +
                                   "<tr class='ctr'> " +
                                       "<td class='ctd' style='width: 20%;'><p><b>Especialidad y/o Subespecialidad:</b></p></td> " +
                    "<td class='ctd' style='width: 60%;'><p>" + esp + "</p></td>" +
                                   "</tr>" +
                                   "<tr class='ctr'> " +
                                       "<td class='ctd' style='width: 20%;'><p><b>Cédula profesional:</b></p></td> " +
                    "<td class='ctd' style='width: 60%;'><p>" + cedprof + "</p></td>" +
                                   "</tr>" +
                                   "<tr class='ctr'> " +
                                       "<td class='ctd' style='width: 20%;'><p><b>Cédula de especialidad:</b></p></td> " +
                    "<td class='ctd' style='width: 60%;'><p>" + cedesp + "</p></td>" +
                                   "</tr>" +
                                   "<tr class='ctr'> " +
                                       "<td class='ctd' style='width: 20%;'><p><b>Diplomados u otros estudios:</b></p></td> " +
                    "<td class='ctd' style='width: 60%;'><p>" + diplo + "</p></td>" +
                                   "</tr>" +
                          "</table>"
                ,
                CSSFont = "table { font-family: Arial; font-size: 10px; }" +
                            ".ctable{  border-style: solid; border-width: 1px; border-color: #C0C0C0;  width: 100%; border-collapse: collapse; }" +
                            ".ctr{ border-style: solid;     border-width: 1px; border-color: #C0C0C0;  width: 100%; border-collapse: collapse; }" +
                          ".ctd{ border-style: solid;       border-width: 1px; border-color: #C0C0C0;  width: 100%; border-collapse: collapse; }"

            });

            Sections.Add(new Section
            {
                Text = "<table cellpadding='5' class='ctable'>" +
                                   "<tr class='ctr'> " +
                                       "<td class='ctd' style='width: 20%;'><p><b>Límite Máximo de Responsabilidad</b></p></td> " +
                    "<td class='ctd' style='width: 60%;'><p>Límite Máximo de Responsabilidad Único y Máximo en el agregado anual incluyendo Gastos de Defensa Juridica: " + sumaaseg+ "</p><br/>" +
                                       "<p><b>Sublímite por Concepto de Daño Moral:</b> hasta el 50% del límite máximo de responsabilidad por evento y en el agregado anual contratado en la presente póliza.</p>" +
                                       "</td>" +
                                   "</tr>" +

                                   "<tr class='ctr'>" +
                                   "<td class='ctd' style='width: 20%;'><p><b>Coberturas</b></p></td> " +
                                   "<td class='ctd' style='width: 60%;'>" +
                                   "<p>" +
                                         "<ul style='list-style-type:Disc'> " +
                                         "<li><b>Responsabilidad Civil Profesional.</b></li>" +
                                         "<li><b>Responsabilidad Civil por el uso de objetos peligrosos (objetiva)</b></li>" +
                                         "<li><b>Suministros de medicamentos y materiales de curación</b></li>" +
                                         "<li><b>Responsabilidad Civil por sus inmuebles y Responsabilidad Civil por sus Actividades </b></li>" +
                                         "<li><b>Responsabilidad Civil Profesional Médica,</b> por la prestación de sus servicios de la salud, en toda la República Mexicana(Consultorios, torres médicas, Hospitales Públicos y Privados, en general todo su ejercicio profesional)</li>" +
                                         "<li><b>Responsabilidad Civil inmuebles,</b> derivada del uso y posesión del inmueble de su consultorio.</li>" +
                                         "<li><b>Responsabilidad Civil por sus actividades,</b> por ejemplo: Por el uso de piscinas, baños, gimnasios, aparatos de rehabilitación física, etc.relacionados con las terapias o rehabilitaciones.</li>" +
                                         "<li><b>Responsabilidad por el uso de objetos peligrosos</b> (“objetiva”). Responsabilidad por el uso de los mecanismos, instrumentos, aparatos o substancias peligrosos por sí mismos y que dan lugar a responsabilidad civil. Estos aparatos pueden ser todos los usados para fines del diagnóstico o de la terapéutica, en cuanto estén reconocidos por la ciencia médica incluyendo el uso de fármacos y materiales de curación.</li>" +
                                         "<li><b>Ampliación a empleados y trabajadores del asegurado.</b> El presente seguro se amplía a cubrir la responsabilidad profesional legal de sus empleados y trabajadores en el desempeño de sus funciones derivadas de la actividad materia de este seguro, que se indica en la cédula de esta póliza. Para los efectos de esta póliza el máximo de empleados, bajo relación de trabajo, que cubre este seguro es de hasta, en consideración del ejercicio privado de un profesionista médico:<br/>" +
                                                "<p>&nbsp;&nbsp;&nbsp;&nbsp;• 2 secretarias o enfermeras-secretarias</p><br/>" +
                                                "<p>&nbsp;&nbsp;&nbsp;&nbsp;• 2 médicos auxiliares</p><br/>" +
                                                "<p>&nbsp;&nbsp;&nbsp;&nbsp;• 3 enfermeras</p><br/>" +
                                         "</li>" +
                                         "<li><b>Medico sustituto.</b>También el presente seguro se amplía a cubrir la responsabilidad cuando al Asegurado, por ausencias temporales encargue a un médico sustituto, de su misma especialidad, la atención de sus pacientes.</li>" +
                                         "<li>Esta cobertura se amplía, en su caso, para <b>herederos legales y cónyuges,</b> en caso de fallecimiento del asegurado y que se interponga reclamación a la masa hereditaria o a la sociedad conyugal respectivamente.</li>" +
                                         "<li><b>Plan de asistencia Legal,</b> adicional y sin costo, anexo condiciones particulares.</li>" + cober_rcArrend +
                                         "</ul>" +
                                   "</p>" +
                                   "</td>" +
                                   "</tr>" +

                                    "<tr  class='ctr'>" +
                                        "<td class='ctd'style='width: 20%;'><p><b>Riesgo Asegurado</b></p></td> " +
                                        "<td class='ctd'style='width: 60%;'> " +
                                            "<p>GMX Seguros se obliga a pagar la indemnización que el Asegurado deba a sus pacientes o a terceros dañados a consecuencia de uno o más hechos realizados sin dolo, ya sea por culpa negligente o imperita (acciones u omisiones), <b>o por el uso de cosas peligrosas, en la prestación de Servicios de Atención Médica.</b></p><br/>" +
                                            "<p>También queda incluida dentro de esta última la responsabilidad por el uso de mecanismos, instrumentos, aparatos o substancias peligrosas por sí mismos y que dan lugar a responsabilidad civil. Estos aparatos pueden ser todos los usados para fines del diagnóstico y de la terapéutica, en cuanto estén reconocidos por la ciencia médica, y causen un daño previsto en esta póliza a terceras personas con motivo de la prestación de servicios para la salud.</p><br/>" +
                                            "<p>Los daños amparados bajo la cobertura de responsabilidad civil comprenden: lesiones corporales, enfermedades, muerte, así como el deterioro o destrucción de bienes. Los perjuicios que resulten y el daño moral, <b>el cual se otorgará bajo un sublímite de 50% del límite máximo de responsabilidad contratado en la presente póliza;</b> los cuales sólo se cubren cuando sean consecuencia directa e inmediata de los citados daños y no como una agravante calificada por un juzgador, con las cuales haya actuado para la realización del daño, incluso cuando dichas agravantes sean considerados como parte de una indemnización identificada bajo el rubro o  el concepto de daño moral.</p><br/>" +
                                        "</td> " +
                                    "</tr> " +

                                   "<tr class='ctr'> " +
                                        "<td class='ctd' style='width: 20%;'><p><b>Base de Indemnización integrantes nuevos:</b></p></td> " +
                                        "<td class='ctd' style='width: 60%;'>" +
                                            "<p>Conforme a lo dispuesto en el Art. 145 bis de la ley sobre el Contrato de Seguro, el presente seguro cubre la indemnización que el Asegurado deba a un tercero, conforme a las condiciones pactadas en el presente contrato, por  hechos ocurridos durante la vigencia de la póliza, siempre que la reclamación se formule por primera vez y por escrito al Asegurado o a GMX Seguros, en el curso de la vigencia de esta póliza o dentro del siguiente año a su terminación.</p>" +
                                        "</td>" +
                                   "</tr>" +

                                   "<tr class='ctr'>" +
                                   "<td class='ctd' style='width: 20%;'><p><b>Exclusiones:</b></p></td> " +
                                   "<td class='ctd' style='width: 60%;'>" +
                                   "<p>" +
                                         "<ul style='list-style-type:Disc'> " +
                                            "<li><b>Acciones dolosas de parte del asegurado</b></li>" +
                                            "<li><b>Garantía de calidad del servicio.</b></li>" +
                                            "<li><b>Incumplimiento de contratos.</b></li>" +
                                            "<li><b>Obligaciones derivadas de un contrato.</b></li>" +
                                            "<li><b>R.C. Productos, incluyendo dentro de éstos los farmacéuticos y transgénicos.</b></li>" +
                                            "<li><b>R.C. Patronal.</b></li>" +
                                            "<li><b>R.C. Contractual.</b></li>" +
                                            "<li><b>Demás que señalan las condiciones generales</b></li>" +
                                         "</ul>" +
                                   "</p>" +
                                   "</td>" +
                                   "</tr>" +

                                   "<tr class='ctr'>" +
                                   "<td class='ctd' style='width: 20%;'><p><b>Deducibles:</b></p></td> " +
                                   "<td class='ctd' style='width: 60%;'>" +
                                   "<p>" +
                                         "<ul style='list-style-type:Disc'> " +
                                            "<li>Para daños a terceros en sus personas: Sin deducible.</li>" +
                                            "<li>Para daños a terceros en sus propiedades: Sin deducible.</li>" +
                                         "</ul>" +
                                   "</p>" +
                                   "</td>" +
                                   "</tr>" +

                                    "<tr class='ctr'> " +
                                        "<td class='ctd' style='width: 20%;'><p><b>Funciones del seguro</b></p></td> " +
                                        "<td class='ctd' style='width: 80%;'>" +
                                                                       "<p><b>Función Indemnizatoria</b></p>" +
                                                                       "<p>Que deba el Asegurado a su paciente derivado de una negligencia, impericia o el uso de sustancias, aparatos u objetos que por su propia naturaleza causen un daño al mismo, así como el de perjuicios y daño moral, siempre y cuando sea a consecuencia directa e inmediata del daño causado.</p><br/>" +
                                                                       "<p>Los daños comprenden lesiones corporales, enfermedades, muerte, así como el deterioro o destrucción de bienes.</p><br/>" +

                                                                       "<p><b>Función de Análisis y defensa legal</b></p>" +
                                                                       "<p>Queda a cargo de GMX Seguros y dentro del límite de responsabilidad asegurado en esta póliza el pago de los gastos de defensa legal del Asegurado.</p><br/>" +
                                                                       "<p>Dichos gastos incluyen la tramitación judicial, así como el análisis de las reclamaciones de terceros, aun cuando ellas sean infundadas, y las cauciones y primas de fianzas requeridas procesalmente.</p><br/>" +

                                                                       "<p><b>*** Se entenderá que la función de análisis y defensa jurídica corresponde única y exclusivamente a GMX Seguros y operará bajo los lineamentos que establezca en su dirección y control del siniestro.</b></p>" +
                                        "</td>" +
                                    "</tr> " +
                #region Condiciones Especiales
                                    //"<tr class='ctr'>" +
                                    //"<td class='ctd' style='width: 20%;'><p><b>Condiciones Especiales:</b></p></td> " +
                                    //"<td class='ctd' style='width: 60%;'>" +
                                    //     "<p>Todos los términos y condiciones conforme al texto Seguro de responsabilidad civil y de responsabilidad civil profesional para profesiones médicas y sus profesiones auxiliares y técnicas.</p>" +
                                    //"</td>" +
                                    //"</tr>" +
                #endregion


                                    "<tr class='ctr'> " +
                                        "<td class='ctd' style='width: 20%;'><p><b>Territorialidad y Jurisdicción</b></p></td> " +
                                        "<td class='ctd' style='width: 80%;'>" +
                                                                       "<p>La responsabilidad civil materia del seguro se determina conforme a la legislación vigente en los Estados Unidos Mexicanos.</p><br/>" +
                                        "</td>" +
                                    "</tr> " +

                          "</table>"
                ,
                CSSFont = "table { font-family: Arial; font-size: 10px; }" +
                            ".ctable{  border-style: solid; border-width: 1px; border-color: #C0C0C0;  width: 100%; border-collapse: collapse; }" +
                            ".ctr{ border-style: solid;     border-width: 1px; border-color: #C0C0C0;  width: 100%; border-collapse: collapse; }" +
                          ".ctd{ border-style: solid;       border-width: 1px; border-color: #C0C0C0;  width: 100%; border-collapse: collapse; }"

            });

            Sections.Add(new Section
            {
                Text = "<br/><p><b>- Condiciones Especiales:</b></p><br/>" +
            "<p>-Médico Sustituto</p><br/>" +
            "<p>Se especifica que en referencia a la cobertura de sustitución provisional mencionado  en el Condicionado General de la presente póliza en el Capítulo III, clausula única de Responsabilidad Civil Profesional, punto 4, inciso b,  se especifica lo siguiente:</p><br/>" +
            "<p>b) El presente seguro se amplía a cubrir la responsabilidad cuando al Asegurado, por ausencias temporales encargue a un médico sustituto, de su misma especialidad, la atención de sus pacientes.</p><br/>" +
            "<p>Dicha condición aplicará siempre y cuando la ausencia de dicho asegurado se origine por su participación en congresos, vacaciones o motivos de fuerza mayor y el paciente tenga el pleno conocimiento y de su consentimiento de ser atendido por el médico sustituto; no será materia de cobertura , en caso de que por cualquier circunstancia, se tome la determinación de realizar algún procedimiento quirúrgico  que no cuente con los puntos mencionados anteriormente, en estos casos la compañía aseguradora  no tendrá ninguna responsabilidad, ni hará frente a la  reclamación derivada de dicha circunstancia.</p><br/>" +
            "<p><b>“VALOR AGREGADO “Plan de asistencia legal”:</b></p><br/>" +
            "<p><b>Como parte de la cobertura queda incluido el valor agregado denominado “Plan de asistencia legal”, bajo los términos y condiciones particulares y folleto que se anexan a la presente póliza, se aclara que este servicio es adicional e independiente a la cobertura de la presente póliza, por lo tanto no afectará la suma asegurada contratada, lo anterior en caso de que sea utilizada por el asegurado.”:</b></p>",
                CSSFont = "p { font-family: Arial; font-size: 10px; text-align: justify; }"
            });

        }

        public static void SlipTradicionalRenov(string numpoliza, bool rc, string nombrecliente, string desc, string esp, string cedprof, string cedesp, string diplo, string sumaaseg, string fecretro, string pol1, string pol2, string pol3)
        {
            docPDF = new DocumentPDF();
            Sections = new List<Section>();

            string Header = "<center><table><tr><td><br/><h2>ESPECIFICACIÓN QUE SE ADHIERE Y FORMA PARTE INTEGRANTE DE LA PÓLIZA</h2><h2>" + numpoliza + "</h2><br/><br/></td></tr></table></center><br/><br/>";
            docPDF.PVLID4CondGrales = "PVLM3D";
            docPDF.StaticText = Header;
            docPDF.StaticCSS = "h2{ font-family: 'Segoe UI'; font-size: small; text-align: center; color: #9c9c9c; } h3{ font-family: 'Segoe UI'; font-size: xx-small; text-align: center; color: #9c9c9c; } table{border: 0px solid #d0d0d0;  margin: auto; width:100 %; }";

            docPDF.StaticXPosition = 110; // Izq/Der
            docPDF.StaticYPosition = 700; // Arriba/abajo

            //Pie de Pagina
            docPDF.PageFoot = "PVL ñ GMX SEGUROS";
            docPDF.PageFootx = 150;

            //Pie de Pagina Numeros
            docPDF.PageNum = true;
            docPDF.mTop = 140;
            docPDF.mBottom = 70;
            docPDF.mRight = 58;
            docPDF.mLeft = 58;

            string cober_rcArrend = rc ? "<li><b>Responsabilidad Civil Arrendatario</b></li>" : string.Empty;
            List<string> tblPolAnteriores = new List<string>();

            if (!String.IsNullOrEmpty(pol1))
                tblPolAnteriores.Add("<tr class='ctr'><td class='ctd' style='width: 20%;'><p><b>No.de póliza 1: </b></p></td><td class='ctd' style='width: 60%;'><p>" + pol1 + "</p></td></tr>");
            if (!String.IsNullOrEmpty(pol2))
                tblPolAnteriores.Add("<tr class='ctr'><td class='ctd' style='width: 20%;'><p><b>No.de póliza 2: </b></p></td><td class='ctd' style='width: 60%;'><p>" + pol2 + "</p></td></tr>");
            if (!String.IsNullOrEmpty(pol3))
                tblPolAnteriores.Add("<tr class='ctr'><td class='ctd' style='width: 20%;'><p><b>No.de póliza 3: </b></p></td><td class='ctd' style='width: 60%;'><p>" + pol3 + "</p></td></tr>");

            Sections.Add(new Section
            {
                Text = "<table cellpadding='5' class='ctable'>" +
                                   "<tr class='ctr'> " +
                                       "<td class='ctd' style='width: 20%;'><p><b>Asegurado:</b></p></td> " +
                    "<td class='ctd' style='width: 60%;'><p>" + nombrecliente + "</p></td>" +
                                   "</tr>" +
                                   "<tr class='ctr'> " +
                                       "<td class='ctd' style='width: 20%;'><p><b>Especificación:</b></p></td> " +
                    "<td class='ctd' style='width: 60%;'><p>" + desc + "</p></td>" +
                                   "</tr>" +
                                   "<tr class='ctr'> " +
                                       "<td class='ctd' style='width: 20%;'><p><b>Especialidad y/o Subespecialidad:</b></p></td> " +
                    "<td class='ctd' style='width: 60%;'><p>" + esp + "</p></td>" +
                                   "</tr>" +
                                   "<tr class='ctr'> " +
                                       "<td class='ctd' style='width: 20%;'><p><b>Cédula profesional:</b></p></td> " +
                    "<td class='ctd' style='width: 60%;'><p>" + cedprof + "</p></td>" +
                                   "</tr>" +
                                   "<tr class='ctr'> " +
                                       "<td class='ctd' style='width: 20%;'><p><b>Cédula de especialidad:</b></p></td> " +
                    "<td class='ctd' style='width: 60%;'><p>" + cedesp + "</p></td>" +
                                   "</tr>" +
                                   "<tr class='ctr'> " +
                                       "<td class='ctd' style='width: 20%;'><p><b>Diplomados u otros estudios:</b></p></td> " +
                    "<td class='ctd' style='width: 60%;'><p>" + diplo + "</p></td>" +
                                   "</tr>" +
                                   "<tr class='ctr'> " +
                                       "<td class='ctd' style='width: 20%;'><p><b>Fecha Convencional</b></p></td> " +
                    "<td class='ctd' style='width: 60%;'><p>" +  fecretro + "</p></td>" +
                                   "</tr>" +
                                   string.Join("", tblPolAnteriores.ToArray())
                                   +
                          "</table>"
                ,
                CSSFont = "table { font-family: Arial; font-size: 10px; }" +
                            ".ctable{  border-style: solid; border-width: 1px; border-color: #C0C0C0;  width: 100%; border-collapse: collapse; }" +
                            ".ctr{ border-style: solid;     border-width: 1px; border-color: #C0C0C0;  width: 100%; border-collapse: collapse; }" +
                          ".ctd{ border-style: solid;       border-width: 1px; border-color: #C0C0C0;  width: 100%; border-collapse: collapse; }"

            });

            Sections.Add(new Section
            {
                Text = "<br/><table cellpadding='5' class='ctable'>" +
                                   "<tr class='ctr'> " +
                                       "<td class='ctd' style='width: 20%;'><p><b>Límite Máximo de Responsabilidad</b></p></td> " +
                    "<td class='ctd' style='width: 60%;'><p>Límite Máximo de Responsabilidad Único y Máximo en el agregado anual incluyendo Gastos de Defensa Juridica: " + sumaaseg+ "</p><br/>" +
                                       "<p><b>Sublímite por Concepto de Daño Moral:</b> hasta el 50% del límite máximo de responsabilidad por evento y en el agregado anual contratado en la presente póliza.</p>" +
                                       "</td>" +
                                   "</tr>" +
                                   "<tr class='ctr'>" +
                                   "<td class='ctd' style='width: 20%;'><p><b>Coberturas</b></p></td> " +
                                   "<td class='ctd' style='width: 60%;'>" +
                                   "<p>" +
                                         "<ul style='list-style-type:Disc'> " +
                                         "<li><b>Responsabilidad Civil Profesional.</b></li>" +
                                         "<li><b>Responsabilidad Civil por el uso de objetos peligrosos (objetiva)</b></li>" +
                                         "<li><b>Suministros de medicamentos y materiales de curación</b></li>" +
                                         "<li><b>Responsabilidad Civil por sus inmuebles y Responsabilidad Civil por sus Actividades </b></li>" +
                                         "<li><b>Responsabilidad Civil Profesional Médica,</b> por la prestación de sus servicios de la salud, en toda la República Mexicana(Consultorios, torres médicas, Hospitales Públicos y Privados, en general todo su ejercicio profesional)</li>" +
                                         "<li><b>Responsabilidad Civil inmuebles,</b> derivada del uso y posesión del inmueble de su consultorio.</li>" +
                                         "<li><b>Responsabilidad Civil por sus actividades,</b> por ejemplo: Por el uso de piscinas, baños, gimnasios, aparatos de rehabilitación física, etc.relacionados con las terapias o rehabilitaciones.</li>" +
                                         "<li><b>Responsabilidad por el uso de objetos peligrosos</b> (“objetiva”). Responsabilidad por el uso de los mecanismos, instrumentos, aparatos o substancias peligrosos por sí mismos y que dan lugar a responsabilidad civil. Estos aparatos pueden ser todos los usados para fines del diagnóstico o de la terapéutica, en cuanto estén reconocidos por la ciencia médica incluyendo el uso de fármacos y materiales de curación.</li>" +
                                         "<li><b>Ampliación a empleados y trabajadores del asegurado.</b> El presente seguro se amplía a cubrir la responsabilidad profesional legal de sus empleados y trabajadores en el desempeño de sus funciones derivadas de la actividad materia de este seguro, que se indica en la cédula de esta póliza. Para los efectos de esta póliza el máximo de empleados, bajo relación de trabajo, que cubre este seguro es de hasta, en consideración del ejercicio privado de un profesionista médico:<br/>" +
                                                "<p>&nbsp;&nbsp;&nbsp;&nbsp;• 2 secretarias o enfermeras-secretarias</p><br/>" +
                                                "<p>&nbsp;&nbsp;&nbsp;&nbsp;• 2 médicos auxiliares</p><br/>" +
                                                "<p>&nbsp;&nbsp;&nbsp;&nbsp;• 3 enfermeras</p><br/>" +
                                         "</li>" +
                                         "<li><b>Medico sustituto.</b>También el presente seguro se amplía a cubrir la responsabilidad cuando al Asegurado, por ausencias temporales encargue a un médico sustituto, de su misma especialidad, la atención de sus pacientes.</li>" +
                                         "<li>Esta cobertura se amplía, en su caso, para <b>herederos legales y cónyuges,</b> en caso de fallecimiento del asegurado y que se interponga reclamación a la masa hereditaria o a la sociedad conyugal respectivamente.</li>" +
                                         "<li><b>Plan de asistencia Legal,</b> adicional y sin costo, anexo condiciones particulares.</li>" + cober_rcArrend +
                                         "</ul>" +
                                   "</p>" +
                                   "</td>" +
                                   "</tr>" +

                                    "<tr  class='ctr'>" +
                                        "<td class='ctd'style='width: 20%;'><p><b>Riesgo Asegurado</b></p></td> " +
                                        "<td class='ctd'style='width: 60%;'> " +
                                            "<p>GMX Seguros se obliga a pagar la indemnización que el Asegurado deba a sus pacientes o a terceros dañados a consecuencia de uno o más hechos realizados sin dolo, ya sea por culpa negligente o imperita (acciones u omisiones), <b>o por el uso de cosas peligrosas, en la prestación de Servicios de Atención Médica.</b></p><br/>" +
                                            "<p>También queda incluida dentro de esta última la responsabilidad por el uso de mecanismos, instrumentos, aparatos o substancias peligrosas por sí mismos y que dan lugar a responsabilidad civil. Estos aparatos pueden ser todos los usados para fines del diagnóstico y de la terapéutica, en cuanto estén reconocidos por la ciencia médica, y causen un daño previsto en esta póliza a terceras personas con motivo de la prestación de servicios para la salud.</p><br/>" +
                                            "<p>Los daños amparados bajo la cobertura de responsabilidad civil comprenden: lesiones corporales, enfermedades, muerte, así como el deterioro o destrucción de bienes. Los perjuicios que resulten y el daño moral, <b>el cual se otorgará bajo un sublímite de 50% del límite máximo de responsabilidad contratado en la presente póliza;</b> los cuales sólo se cubren cuando sean consecuencia directa e inmediata de los citados daños y no como una agravante calificada por un juzgador, con las cuales haya actuado para la realización del daño, incluso cuando dichas agravantes sean considerados como parte de una indemnización identificada bajo el rubro o  el concepto de daño moral.</p><br/>" +
                                        "</td> " +
                                    "</tr> " +

                                   "<tr class='ctr'> " +
                                        "<td class='ctd' style='width: 20%;'><p><b>Base de Indemnización integrantes nuevos:</b></p></td> " +
                                        "<td class='ctd' style='width: 60%;'>" +
                                            "<p>De acuerdo con lo previsto en el art. 145 bis de la Ley sobre el Contrato de Seguro, se otorga fecha convencional desde el inicio de vigencia de la primera póliza contratada con GMX Seguros, siempre que las renovaciones hayan sido una cadena ininterrumpida de seguros con GMX Seguros, ello sobre hechos no conocidos ni reclamados previamente al Asegurado o a GMX Seguros y siempre que la reclamación se formule por primera vez y por escrito al Asegurado o a GMX Seguros, durante la vigencia actual de la póliza. Se aclara que en caso de reclamación, aplicarán los límites y las condiciones que prevalecen en la póliza que corresponde al año de la reclamación, es decir, las condiciones de la póliza vigente en el momento de la presentación de la reclamación al asegurado o a GMX Seguros, lo que ocurra primero, sujetándose al marco de las cláusulas de la citada póliza.</p><br/>" +
                                            "<p>Se aclara además que en este caso las disposiciones del Preámbulo y de la letra b) de la Cláusula 1ª del Capítulo I de la póliza (condiciones generales) se modifican para quedar como sigue:</p><br/>" +
                                            "<p>“Preámbulo: El presente contrato de seguro se celebra conforme a lo dispuesto en el inciso b) del Art. 145 bis de la Ley sobre el Contrato de Seguros, para cubrir la indemnización que el Asegurado deba a un tercero por hechos ocurridos desde la fecha convencional, siempre que la reclamación se formule por primera vez y por escrito al Asegurado o a GMX Seguros, en el curso de la vigencia de la póliza.”</p><br/>" +
                                            "<p>Capítulo I, Cláusula 1ª:</p>" +
                                            "<p>“b) Base de indemnización.</p>" +
                                            "<p>El presente seguro cubre la indemnización que el Asegurado deba a un tercero, conforme a las condiciones pactadas en el presente contrato, por hechos ocurridos desde la fecha convencional, siempre que la reclamación se formule por primera vez y por escrito al Asegurado o a GMX Seguros, en el curso de la vigencia de esta póliza.”</p><br/>" +
                                            "<p><b>FECHA CONVENCIONAL:</b></p><br/>" +
                                            "<p>De acuerdo con lo previsto en el art. 145 bis de la Ley sobre el Contrato de Seguro, se otorga fecha convencional desde el inicio de vigencia de la primera póliza contratada con GMX Seguros, siempre que las renovaciones hayan sido una cadena ininterrumpida de seguros con GMX Seguros, ello sobre hechos no conocidos ni reclamados previamente al Asegurado o a GMX Seguros y siempre que la reclamación se formule por primera vez y por escrito al Asegurado o a GMX Seguros, durante la vigencia actual de la póliza.</p><br/>" +
                                            "<p>Se aclara que en caso de reclamación, aplicarán los límites y las condiciones que prevalecen en la póliza que corresponde al año del hecho generador que propicia la reclamación, es decir, las condiciones de la póliza vigente en el momento del hecho generador del daño, sujetándose al marco de las cláusulas de la citada póliza.</p><br/>" +
                                            "<p><b>NOTA IMPORTANTE: Para que la fecha convencional sea reconocida será necesario que el asegurado acredite la continuidad de pólizas pagadas con GMX Seguros sin periodos descubiertos.</b></p><br/>" +
                                        "</td>" +
                                   "</tr>" +

                                   "<tr class='ctr'>" +
                                   "<td class='ctd' style='width: 20%;'><p><b>Exclusiones:</b></p></td> " +
                                   "<td class='ctd' style='width: 60%;'>" +
                                   "<p>" +
                                         "<ul style='list-style-type:Disc'> " +
                                            "<li><b>Acciones dolosas de parte del asegurado</b></li>" +
                                            "<li><b>Garantía de calidad del servicio.</b></li>" +
                                            "<li><b>Incumplimiento de contratos.</b></li>" +
                                            "<li><b>Obligaciones derivadas de un contrato.</b></li>" +
                                            "<li><b>R.C. Productos, incluyendo dentro de éstos los farmacéuticos y transgénicos.</b></li>" +
                                            "<li><b>R.C. Patronal.</b></li>" +
                                            "<li><b>R.C. Contractual.</b></li>" +
                                            "<li><b>Demás que señalan las condiciones generales</b></li>" +
                                         "</ul>" +
                                   "</p>" +
                                   "</td>" +
                                   "</tr>" +

                                   "<tr class='ctr'>" +
                                   "<td class='ctd' style='width: 20%;'><p><b>Deducibles:</b></p></td> " +
                                   "<td class='ctd' style='width: 60%;'>" +
                                   "<p>" +
                                         "<ul style='list-style-type:Disc'> " +
                                            "<li>Para daños a terceros en sus personas: Sin deducible.</li>" +
                                            "<li>Para daños a terceros en sus propiedades: Sin deducible.</li>" +
                                         "</ul>" +
                                   "</p>" +
                                   "</td>" +
                                   "</tr>" +
                #region Funciones del seguro
                                    //"<tr class='ctr'>" +
                                    //"<td class='ctd' style='width: 20%;'><p><b>Funciones del seguro:</b></p></td> " +
                                    //"<td class='ctd' style='width: 60%;'>" +
                                    //"<p>" +
                                    //      "<ul style='list-style-type:Disc'> " +
                                    //         "<li><b>Función indemnizatoria,</b> que deba el asegurado a su paciente derivado de una negligencia, impericia o el uso de sustancias, aparatos u objetos que por su propia naturaleza causen un daño al mismo, así como el los perjuicios y daño moral, siempre y cuando sea a consecuencia directa e inmediata del daño causado.</li>" +
                                    //      "</ul>" +
                                    //      "<br/><br/><p>Los daños comprenden lesiones corporales, enfermedades, muerte, así como el deterioro o destrucción de bienes.</p><br/>" +
                                    //       "<ul style='list-style-type:Disc'> " +
                                    //         "<li><b>Función de análisis y defensa legal,</b> reclamaciones judiciales y extrajudiciales, hechas al asegurado directamente o por medio de autoridades Administrativas (CONAMED, OIC, Derechos Humanos, Etc.), Juzgados Civiles (Materia Civil), Ministerios Públicos y Juzgados de lo Penal, local o federal (Materia Penal), incluye fianzas y cauciones que se requieran en su caso.</li>" +
                                    //      "</ul>" +
                                    //"</p>" +
                                    //"</td>" +
                                    //"</tr>" +
                #endregion


                                    "<tr class='ctr'> " +
                                        "<td class='ctd' style='width: 20%;'><p><b>Funciones del seguro</b></p></td> " +
                                        "<td class='ctd' style='width: 80%;'>" +
                                                                       "<p><b>Función Indemnizatoria</b></p>" +
                                                                       "<p>Que deba el Asegurado a su paciente derivado de una negligencia, impericia o el uso de sustancias, aparatos u objetos que por su propia naturaleza causen un daño al mismo, así como el de perjuicios y daño moral, siempre y cuando sea a consecuencia directa e inmediata del daño causado.</p><br/>" +
                                                                       "<p>Los daños comprenden lesiones corporales, enfermedades, muerte, así como el deterioro o destrucción de bienes.</p><br/>" +

                                                                       "<p><b>Función de Análisis y defensa legal</b></p>" +
                                                                       "<p>Queda a cargo de GMX Seguros y dentro del límite de responsabilidad asegurado en esta póliza el pago de los gastos de defensa legal del Asegurado.</p><br/>" +
                                                                       "<p>Dichos gastos incluyen la tramitación judicial, así como el análisis de las reclamaciones de terceros, aun cuando ellas sean infundadas, y las cauciones y primas de fianzas requeridas procesalmente.</p><br/>" +

                                                                       "<p><b>*** Se entenderá que la función de análisis y defensa jurídica corresponde única y exclusivamente a GMX Seguros y operará bajo los lineamentos que establezca en su dirección y control del siniestro.</b></p>" +
                                        "</td>" +
                                    "</tr> " +

                #region Condiciones Especiales
                                    //"<tr class='ctr'>" +
                                    //"<td class='ctd' style='width: 20%;'><p><b>Condiciones Especiales:</b></p></td> " +
                                    //"<td class='ctd' style='width: 60%;'>" +
                                    //     "<p>Todos los términos y condiciones conforme al texto Seguro de responsabilidad civil y de responsabilidad civil profesional para profesiones médicas y sus profesiones auxiliares y técnicas.</p>" +
                                    //"</td>" +
                                    //"</tr>" +
                #endregion

                                    "<tr class='ctr'> " +
                                        "<td class='ctd' style='width: 20%;'><p><b>Territorialidad y Jurisdicción</b></p></td> " +
                                        "<td class='ctd' style='width: 80%;'>" +
                                                                       "<p>La responsabilidad civil materia del seguro se determina conforme a la legislación vigente en los Estados Unidos Mexicanos.</p><br/>" +
                                        "</td>" +
                                    "</tr> " +
                          "</table>"
                ,
                CSSFont = "table { font-family: Arial; font-size: 10px; }" +
                            ".ctable{  border-style: solid; border-width: 1px; border-color: #C0C0C0;  width: 100%; border-collapse: collapse; }" +
                            ".ctr{ border-style: solid;     border-width: 1px; border-color: #C0C0C0;  width: 100%; border-collapse: collapse; }" +
                          ".ctd{ border-style: solid;       border-width: 1px; border-color: #C0C0C0;  width: 100%; border-collapse: collapse; }"

            });
            Sections.Add(new Section
            {
                Text = "<br/><p><b>- Condiciones Especiales:</b></p><br/>" +
            "<p>-Médico Sustituto</p><br/>" +
            "<p>Se especifica que en referencia a la cobertura de sustitución provisional mencionado  en el Condicionado General de la presente póliza en el Capítulo III, clausula única de Responsabilidad Civil Profesional, punto 4, inciso b,  se especifica lo siguiente:</p><br/>" +
            "<p>b) El presente seguro se amplía a cubrir la responsabilidad cuando al Asegurado, por ausencias temporales encargue a un médico sustituto, de su misma especialidad, la atención de sus pacientes.</p><br/>" +
            "<p>Dicha condición aplicará siempre y cuando la ausencia de dicho asegurado se origine por su participación en congresos, vacaciones o motivos de fuerza mayor y el paciente tenga el pleno conocimiento y de su consentimiento de ser atendido por el médico sustituto; no será materia de cobertura , en caso de que por cualquier circunstancia, se tome la determinación de realizar algún procedimiento quirúrgico  que no cuente con los puntos mencionados anteriormente, en estos casos la compañía aseguradora  no tendrá ninguna responsabilidad, ni hará frente a la  reclamación derivada de dicha circunstancia.</p><br/>" +
            "<p><b>“VALOR AGREGADO “Plan de asistencia legal”:</b></p><br/>" +
            "<p><b>Como parte de la cobertura queda incluido el valor agregado denominado “Plan de asistencia legal”, bajo los términos y condiciones particulares y folleto que se anexan a la presente póliza, se aclara que este servicio es adicional e independiente a la cobertura de la presente póliza, por lo tanto no afectará la suma asegurada contratada, lo anterior en caso de que sea utilizada por el asegurado.”:</b></p>",
                CSSFont = "p { font-family: Arial; font-size: 10px; text-align: justify; }"
            });

        }
        #endregion Tradicional


        #region Angeles
        public static void SlipAngeles(string numpoliza, string nombrecliente, string desc, string esp, string cedprof, string cedesp, string diplo, decimal sumaa)
        {
            string amount = String.Format("{0:C} M.N.", sumaa);

            docPDF = new DocumentPDF();
            Sections = new List<Section>();

            string Header = "<center><table><tr><td><br/><h2>ESPECIFICACIÓN QUE SE ADHIERE Y FORMA PARTE INTEGRANTE DE LA PÓLIZA</h2><h2>" + numpoliza + "</h2><br/><br/></td></tr></table></center><br/><br/>";
            docPDF.PVLID4CondGrales = "PVLM3D";
            docPDF.StaticText = Header;
            docPDF.StaticCSS = "h2{ font-family: 'Segoe UI'; font-size: small; text-align: center; color: #9c9c9c; } h3{ font-family: 'Segoe UI'; font-size: xx-small; text-align: center; color: #9c9c9c; } table{border: 0px solid #d0d0d0;  margin: auto; width:100 %; }";

            docPDF.StaticXPosition = 110; // Izq/Der
            docPDF.StaticYPosition = 700; // Arriba/abajo

            //Pie de Pagina
            docPDF.PageFoot = "PVL – GMX SEGUROS";
            docPDF.PageFootx = 150;

            //Pie de Pagina Numeros
            docPDF.PageNum = true;
            docPDF.mTop = 140;
            docPDF.mBottom = 70;
            docPDF.mRight = 58;
            docPDF.mLeft = 58;

            
            Sections.Add(new Section
            {
                Text = "<table cellpadding='5' class='ctable'>" +
                       "<tr class='ctr'> " +
                           "<td class='ctd' style='width: 20%;'><p><b>Asegurado:</b></p></td> " +
                    "<td class='ctd' style='width: 60%;'><p>" + nombrecliente + "</p></td>" +
                       "</tr>" +
                       "<tr class='ctr'> " +
                           "<td class='ctd' style='width: 20%;'><p><b>Especificación:</b></p></td> " +
                    "<td class='ctd' style='width: 60%;'><p>" + desc + "</p></td>" +
                       "</tr>" +
                       "<tr class='ctr'> " +
                           "<td class='ctd' style='width: 20%;'><p><b>Especialidad y/o Subespecialidad:</b></p></td> " +
                           "<td class='ctd' style='width: 60%;'><p>" + esp + "</p></td>" +
                       "</tr>" +
                       "<tr class='ctr'> " +
                           "<td class='ctd' style='width: 20%;'><p><b>Cédula profesional:</b></p></td> " +
                    "<td class='ctd' style='width: 60%;'><p>" + cedprof + "</p></td>" +
                       "</tr>" +
                       "<tr class='ctr'> " +
                           "<td class='ctd' style='width: 20%;'><p><b>Cédula de especialidad:</b></p></td> " +
                    "<td class='ctd' style='width: 60%;'><p>" + cedesp + "</p></td>" +
                       "</tr>" +
                       "<tr class='ctr'> " +
                           "<td class='ctd' style='width: 20%;'><p><b>Diplomados u otros estudios:</b></p></td> " +
                    "<td class='ctd' style='width: 60%;'><p>" + diplo + "</p></td>" +
                       "</tr>" +
              "</table>"
    ,
                CSSFont = "table { font-family: Arial; font-size: 10px; }" +
                ".ctable{  border-style: solid; border-width: 1px; border-color: #C0C0C0;  width: 100%; border-collapse: collapse; }" +
                ".ctr{ border-style: solid;     border-width: 1px; border-color: #C0C0C0;  width: 100%; border-collapse: collapse; }" +
              ".ctd{ border-style: solid;       border-width: 1px; border-color: #C0C0C0;  width: 100%; border-collapse: collapse; }"

            });

            Sections.Add(new Section
            {
                Text = "<br/><table cellpadding='5' class='ctable'>" +
                                   "<tr class='ctr'>" +
                                   "<td class='ctd' style='width: 20%;'><p><b>Coberturas:</b></p></td> " +
                                   "<td class='ctd' style='width: 60%;'>" +
                                   "<p>" +
                                         "<ul style='list-style-type:Disc'> " +
                                         "<li><b>Responsabilidad Profesional.</b></li>" +
                                         "<li><b>Responsabilidad Civil por el uso de objetos peligrosos (objetiva)</b></li>" +
                                         "<li><b>Suministros de medicamentos y materiales de curación</b></li>" +
                                         "<li><b>Responsabilidad Civil por sus inmuebles y Responsabilidad Civil por sus Actividades del consultorio(os) que ocupe para ejercer su profesión.</ b></li>" +
                                         "<li><b>Cobertura automáticamente extendida para la responsabilidad civil y para la responsabilidad civil y profesional de su personal médico como empleados, de acuerdo a las condiciones generales.</ b></li>" +
                                         "</ul>" +
                                   "</p>" +
                                   "</td>" +
                                   "</tr>" +
                                   "<tr class='ctr'>" +
                                   "<td class='ctd' style='width: 20%;'><p><b>Coberturas Especiales sin costo:</b></p></td> " +
                                   "<td class='ctd' style='width: 60%;'>" +
                                   "<p>" +
                                   "Para las siguientes coberturas, están aseguradas, dentro del marco de las condiciones de la póliza, la responsabilidad civil legal en que incurriere el Asegurado por daños a terceros, derivada de las siguientes actividades que enseguida se indican:" +
                                   "</p> <br/>" +
                                   "<p>" +
                                   "-Responsabilidad civil del arrendatario de consultorio." +
                                   "</p>" +
                                   "<p>" +
                                   "Está asegurada la responsabilidad civil legal por daños que, por incendio y rayo o explosión, se causen al inmueble o inmuebles que el Asegurado haya tomado, totalmente o en parte, en arrendamiento, para ser usado por consultorio, siempre que dichos daños le sean imputables." +
                                   "</p> <br/>" +
                                   "<p>" +
                                   "- Responsabilidad civil como condómino de consultorio." +
                                   "</p>" +
                                   "<p>Está asegurada además la responsabilidad civil legal del Asegurado por daños ocasionados a las áreas comunes del condómino en el cual tenga su consultorio; además por daños ocasionados a los condóminos (no incluye las áreas del Asegurado) a consecuencia de un derrame de agua accidental e imprevisto.</p><br/>" +
                                   "<p>Sin embargo, para la indemnización a pagar por GMX Seguros se descontará un porcentaje, equivalente a la cuota del Asegurado como propietario de dichas áreas comunes.</p><br/>" +
                                   "<p>" +
                                   "- Responsabilidad civil privada y familiar." +
                                   "</p>" +
                                   "<p>La Responsabilidad Civil Legal en que incurra el Asegurado por daños a terceros, derivada de las actividades privadas y familiares, en cualquiera de los siguientes supuestos:</p><br/>" +
                                   "<p>" +
                                        "<ol style='list-style-type: lower-alpha; line-height: 130%;'>" +
                                        "<li>Como jefe de familia.</li>" +
                                        "<li>Como propietario de una o varias casas habitación, (incluye las habitadas en fines de semana y en vacaciones).</li>" +
                                        "<li>Por daños ocasionados a consecuencia de incendio, o explosión del inmueble, edificio u hogar.</li>" +
                                        "<li>Por daños a consecuencia de un derrame de agua accidental e imprevisto.</li>" +
                                        "<li>Por práctica de deportes como aficionado.</li>" +
                                        "<li>Por el uso de bicicletas, patines, embarcaciones de pedal o de remo y vehículos no motorizados.</li>" +
                                        "<li>Como propietario de animales domésticos, de caza y Guardianes.</li>" +
                                        "<li>Durante viajes de estudios, de vacaciones o de placer del Asegurado o algún dependiente económico dentro de la República Mexicana.</li>" +
                                        "<li>Durante viajes de estudios, de vacaciones o de placer del Asegurado o algún dependiente económico, en el extranjero. </li>" +
                                        "<li>Se cubre la responsabilidad que resulte ante los trabajadores domésticos por riesgo de trabajo, otorgándose las prestaciones marcadas en el Título IX de la Ley Federal del Trabajo, hasta por un límite de mil DSMGVDF por cada trabajador, máximo dos trabajadores. </li>" +
                                        "<li>Responsabilidad civil como condómino, está asegurada además la responsabilidad civil legal del Asegurado por daños ocasionados a las áreas comunes del condómino en el cual tenga su consultorio; sin embargo, la indemnización a pagar por GMX Seguros se descontará un porcentaje, equivalente a la cuota del Asegurado como propietario de dichas áreas comunes." +
                                        "</li>" +
                                        "</ol></p> <br/><p>" +
                                            "<ul style='list-style-type: disc; line-height: 130%;'>" +
                                                "<li>El límite máximo de responsabilidad opera como límite único y combinado, en el agregado anual., incluso sí posee más de un domicilio.</li>" +
                                                "<li>Límite de Responsabilidad Único y Máximo en el agregado anual incluyendo Gastos de Defensa Jurídica</li>" +
                                            "</ul>" +
                                    "</p>" +
                                   "</td>" +
                                   "</tr>" +

                                   "<tr class='ctr'>" +
                                   "<td class='ctd' style='width: 20%;'><p><b>Límite Máximo de Responsabilidad:</b></p></td> " +
                                   "<td class='ctd' style='width: 60%;'>" +
                                   "<p>" +
                                   "<ul style='list-style: disc; line-height: 130%;'>" +
                                        "<li>   Límite de Responsabilidad Único y Máximo en el agregado anual incluyendo Gastos de Defensa Jurídica: " + amount + "</li>" +
                                   "</ul>" +
                                    "<br/><p><b>Sublímite por Concepto de Daño Moral:</b> hasta el 50% del límite máximo de responsabilidad por evento y en el agregado anual contratado en la presente póliza.</p>" +
                                   "</p>" +
                                   "</td>" +
                                   "</tr>" +

                                   "<tr class='ctr'>" +
                                   "<td class='ctd' style='width: 20%;'><p><b>Sublímites:</b></p></td> " +
                                   "<td class='ctd' style='width: 60%;'>" +
                                   "<p><b>Responsabilidad Civil privada y familiar $ 100,000.00 M.N.</b></p><br/>" +
                                   "</td>" +

                                   "</tr>" +

                                   "<tr class='ctr'>" +
                                   "<td class='ctd' style='width: 20%;'><p><b>Deducibles:</b></p></td> " +
                                   "<td class='ctd' style='width: 60%;'>" +
                                   "<p><b>Responsabilidad Civil profesional</b></p><br/>" +
                                   "<p>-    Para daños a terceros en sus personas: Sin deducible.</p>" +
                                   "<p>-    Para daños a terceros en sus propiedades: Sin deducible</p>" +

                                   "<br/><p><b>Responsabilidad Civil privada y familiar</b></p><br/>" +
                                   "<p>-    Responsabilidad Civil en el extranjero: 10% del monto de cada reclamación, con mínimo " + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + " de $3,000.00 M.N.</p>" +
                                   "<p>-    Responsabilidad Civil en la República Mexicana: 10% del monto de cada reclamación, con mínimo de $2,000.00 M.N.</p>" +


                                   "<br/><p><b>Demás riesgos</b></p><br/>" +
                                   "<p>5% del monto de cada reclamación.</p>" +
                                   "</td>" +

                                   "</tr>" +

                                    "<tr  class='ctr'>" +
                                        "<td class='ctd'style='width: 20%;'><p><b>Riesgo Asegurado</b></p></td> " +
                                        "<td class='ctd'style='width: 60%;'> " +
                                            "<p>GMX Seguros se obliga a pagar la indemnización que el Asegurado deba a sus pacientes o a terceros dañados a consecuencia de uno o más hechos realizados sin dolo, ya sea por culpa negligente o imperita (acciones u omisiones), <b>o por el uso de cosas peligrosas, en la prestación de Servicios de Atención Médica.</b></p><br/>" +
                                            "<p>También queda incluida dentro de esta última la responsabilidad por el uso de mecanismos, instrumentos, aparatos o substancias peligrosas por sí mismos y que dan lugar a responsabilidad civil. Estos aparatos pueden ser todos los usados para fines del diagnóstico y de la terapéutica, en cuanto estén reconocidos por la ciencia médica, y causen un daño previsto en esta póliza a terceras personas con motivo de la prestación de servicios para la salud.</p><br/>" +
                                            "<p>Los daños amparados bajo la cobertura de responsabilidad civil comprenden: lesiones corporales, enfermedades, muerte, así como el deterioro o destrucción de bienes. Los perjuicios que resulten y el daño moral, <b>el cual se otorgará bajo un sublímite de 50% del límite máximo de responsabilidad contratado en la presente póliza;</b> los cuales sólo se cubren cuando sean consecuencia directa e inmediata de los citados daños y no como una agravante calificada por un juzgador, con las cuales haya actuado para la realización del daño, incluso cuando dichas agravantes sean considerados como parte de una indemnización identificada bajo el rubro o  el concepto de daño moral.</p><br/>" +
                                        "</td> " +
                                    "</tr> " +

                                   "<tr class='ctr'> " +
                                        "<td class='ctd' style='width: 20%;'><p><b>Base de Indemnización:</b></p></td> " +
                                        "<td class='ctd' style='width: 60%;'>" +
                                            "<p>Conforme a lo dispuesto en el Art. 145 bis de la ley sobre el Contrato de Seguro, el presente seguro cubre la indemnización que el Asegurado deba a un tercero, conforme a las condiciones pactadas en el presente contrato, por hechos ocurridos durante la vigencia de la póliza, siempre que la reclamación se formule por primera vez y por escrito al Asegurado o a GMX Seguros, en el curso de la vigencia de esta póliza o dentro del siguiente año a su terminación.</p>" +
                                        "</td>" +
                                   "</tr>" +

                                    "<tr class='ctr'> " +
                                        "<td class='ctd' style='width: 20%;'><p><b>Funciones del seguro</b></p></td> " +
                                        "<td class='ctd' style='width: 80%;'>" +
                                                                       "<p><b>Función Indemnizatoria</b></p>" +
                                                                       "<p>Que deba el Asegurado a su paciente derivado de una negligencia, impericia o el uso de sustancias, aparatos u objetos que por su propia naturaleza causen un daño al mismo, así como el de perjuicios y daño moral, siempre y cuando sea a consecuencia directa e inmediata del daño causado.</p><br/>" +
                                                                       "<p>Los daños comprenden lesiones corporales, enfermedades, muerte, así como el deterioro o destrucción de bienes.</p><br/>" +

                                                                       "<p><b>Función de Análisis y defensa legal</b></p>" +
                                                                       "<p>Queda a cargo de GMX Seguros y dentro del límite de responsabilidad asegurado en esta póliza el pago de los gastos de defensa legal del Asegurado.</p><br/>" +
                                                                       "<p>Dichos gastos incluyen la tramitación judicial, así como el análisis de las reclamaciones de terceros, aun cuando ellas sean infundadas, y las cauciones y primas de fianzas requeridas procesalmente.</p><br/>" +

                                                                       "<p><b>*** Se entenderá que la función de análisis y defensa jurídica corresponde única y exclusivamente a GMX Seguros y operará bajo los lineamentos que establezca en su dirección y control del siniestro.</b></p>" +
                                        "</td>" +
                                    "</tr> " +

                                   "<tr class='ctr'>" +
                                   "<td class='ctd' style='width: 20%;'><p><b>Exclusiones:</b></p></td> " +
                                   "<td class='ctd' style='width: 60%;'>" +
                                   "<br/><p>" +
                                    "<ul style='list-style-type: square;  line-height: 130%;'>" +
                                        "<li><b>No será efectiva esta cobertura si el Asegurado no acredita su relación o pertenencia como médico de Grupo Angeles</b></li>" +
                                        "<li>Acciones dolosas de parte del asegurado</li>" +
                                        "<li>Garantía de calidad del servicio en cuanto a su cumplimiento en tiempo y forma</li>" +
                                        "<li>Incumplimiento de contratos</li>" +
                                        "<li>Multas, daños punitivos o ejemplares y/o venganza</li>" +
                                        "<li>Responsabilidad por la fabricación de productos farmacéuticos y transgénicos además la participación en estudios farmacológicos o de nuevos recursos.</li>" +
                                        "<li>R.C. Patronal</li>" +
                                        "<li>R.C. Estacionamiento</li>" +
                                    "</ul>" +
                                   "</p>" +
                                   "</td>" +
                                   "</tr>" +

                                   "<tr class='ctr'>" +
                                   "<td class='ctd' style='width: 20%;'><p><b>Jurisdicción:</b></p></td> " +
                                   "<td class='ctd' style='width: 60%;'>" +
                                    "<p>" +
                                        "La responsabilidad civil materia del seguro se determina conforme a la legislación vigente en los Estados Unidos Mexicanos." +
                                   "</p>" +
                                   "</td>" +
                                   "</tr>" +


                              "</table>",
                CSSFont = "table { font-family: Arial; font-size: 10px; }" +
                            ".ctable{  border-style: solid; border-width: 1px; border-color: #C0C0C0;  width: 100%; border-collapse: collapse; }" +
                            ".ctr{ border-style: solid;     border-width: 1px; border-color: #C0C0C0;  width: 100%; border-collapse: collapse; }" +
                          ".ctd{ border-style: solid;       border-width: 1px; border-color: #C0C0C0;  width: 100%; border-collapse: collapse; }"
            });

            Sections.Add(new Section
            {
                Text = "<br/><p><b>- Condiciones Especiales:</b></p><br/>" +
            "<p>-Médico Sustituto</p><br/>" +
            "<p>Se especifica que en referencia a la cobertura de sustitución provisional mencionado  en el Condicionado General de la presente póliza en el Capítulo III, clausula única de Responsabilidad Civil Profesional, punto 4, inciso b,  se especifica lo siguiente:</p><br/>" +
            "<p>b) El presente seguro se amplía a cubrir la responsabilidad cuando al Asegurado, por ausencias temporales encargue a un médico sustituto, de su misma especialidad, la atención de sus pacientes.</p><br/>" +
            "<p>Dicha condición aplicará siempre y cuando la ausencia de dicho asegurado se origine por su participación en congresos, vacaciones o motivos de fuerza mayor y el paciente tenga el pleno conocimiento y de su consentimiento de ser atendido por el médico sustituto; no será materia de cobertura , en caso de que por cualquier circunstancia, se tome la determinación de realizar algún procedimiento quirúrgico  que no cuente con los puntos mencionados anteriormente, en estos casos la compañía aseguradora  no tendrá ninguna responsabilidad, ni hará frente a la  reclamación derivada de dicha circunstancia.</p><br/>" +
            "<p><b>“VALOR AGREGADO “Plan de asistencia legal”:</b></p><br/>" +
            "<p><b>Como parte de la cobertura queda incluido el valor agregado denominado “Plan de asistencia legal”, bajo los términos y condiciones particulares y folleto que se anexan a la presente póliza, se aclara que este servicio es adicional e independiente a la cobertura de la presente póliza, por lo tanto no afectará la suma asegurada contratada, lo anterior en caso de que sea utilizada por el asegurado.”:</b></p>",
                CSSFont = "p { font-family: Arial; font-size: 10px; text-align: justify; }"
            });

        }

        public static void SlipAngelesReov(string numpoliza, string nombrecliente, string desc, string esp, string cedprof, string cedesp, string diplo, decimal sumaa, string fecretro, string pol1, string pol2, string pol3)
        {
            string amount = String.Format("{0:C} M.N.", sumaa);
            docPDF = new DocumentPDF();
            Sections = new List<Section>();

            string Header = "<center><table><tr><td><br/><h2>ESPECIFICACIÓN QUE SE ADHIERE Y FORMA PARTE INTEGRANTE DE LA PÓLIZA</h2><h2>" + numpoliza + "</h2><br/><br/></td></tr></table></center><br/><br/>";
            docPDF.PVLID4CondGrales = "PVLM3D";
            docPDF.StaticText = Header;
            docPDF.StaticCSS = "h2{ font-family: 'Segoe UI'; font-size: small; text-align: center; color: #9c9c9c; } h3{ font-family: 'Segoe UI'; font-size: xx-small; text-align: center; color: #9c9c9c; } table{border: 0px solid #d0d0d0;  margin: auto; width:100 %; }";

            docPDF.StaticXPosition = 110; // Izq/Der
            docPDF.StaticYPosition = 700; // Arriba/abajo

            //Pie de Pagina
            docPDF.PageFoot = "PVL – GMX SEGUROS";
            docPDF.PageFootx = 150;

            //Pie de Pagina Numeros
            docPDF.PageNum = true;
            docPDF.mTop = 140;
            docPDF.mBottom = 70;
            docPDF.mRight = 58;
            docPDF.mLeft = 58;

            List<string> tblPolAnteriores = new List<string>();

            if (!String.IsNullOrEmpty(pol1))
                tblPolAnteriores.Add("<tr class='ctr'><td class='ctd' style='width: 20%;'><p><b>No.de póliza 1: </b></p></td><td class='ctd' style='width: 60%;'><p>" + pol1 + "</p></td></tr>");
            if (!String.IsNullOrEmpty(pol2))
                tblPolAnteriores.Add("<tr class='ctr'><td class='ctd' style='width: 20%;'><p><b>No.de póliza 2: </b></p></td><td class='ctd' style='width: 60%;'><p>" + pol2 + "</p></td></tr>");
            if (!String.IsNullOrEmpty(pol3))
                tblPolAnteriores.Add("<tr class='ctr'><td class='ctd' style='width: 20%;'><p><b>No.de póliza 3: </b></p></td><td class='ctd' style='width: 60%;'><p>" + pol3 + "</p></td></tr>");

            Sections.Add(new Section
            {
                Text = "<table cellpadding='5' class='ctable'>" +
                       "<tr class='ctr'> " +
                           "<td class='ctd' style='width: 20%;'><p><b>Asegurado:</b></p></td> " +
                    "<td class='ctd' style='width: 60%;'><p>" + nombrecliente + "</p></td>" +
                       "</tr>" +
                       "<tr class='ctr'> " +
                           "<td class='ctd' style='width: 20%;'><p><b>Especificación:</b></p></td> " +
                    "<td class='ctd' style='width: 60%;'><p>" + desc + "</p></td>" +
                       "</tr>" +
                       "<tr class='ctr'> " +
                           "<td class='ctd' style='width: 20%;'><p><b>Especialidad y/o Subespecialidad:</b></p></td> " +
                    "<td class='ctd' style='width: 60%;'><p>" + esp + "</p></td>" +
                       "</tr>" +
                       "<tr class='ctr'> " +
                           "<td class='ctd' style='width: 20%;'><p><b>Cédula profesional:</b></p></td> " +
                    "<td class='ctd' style='width: 60%;'><p>" + cedprof + "</p></td>" +
                       "</tr>" +
                       "<tr class='ctr'> " +
                           "<td class='ctd' style='width: 20%;'><p><b>Cédula de especialidad:</b></p></td> " +
                    "<td class='ctd' style='width: 60%;'><p>" + cedesp + "</p></td>" +
                       "</tr>" +
                       "<tr class='ctr'> " +
                           "<td class='ctd' style='width: 20%;'><p><b>Diplomados u otros estudios:</b></p></td> " +
                    "<td class='ctd' style='width: 60%;'><p>" + diplo + "</p></td>" +
                       "</tr>" +
                       "<tr class='ctr'> " +
                           "<td class='ctd' style='width: 20%;'><p><b>Fecha Convencional</b></p></td> " +
                    "<td class='ctd' style='width: 60%;'><p>" + fecretro + "</p></td>" +
                       "</tr>"
                       +
              "</table>"
            ,
                CSSFont = "table { font-family: Arial; font-size: 10px; }" +
                ".ctable{  border-style: solid; border-width: 1px; border-color: #C0C0C0;  width: 100%; border-collapse: collapse; }" +
                ".ctr{ border-style: solid;     border-width: 1px; border-color: #C0C0C0;  width: 100%; border-collapse: collapse; }" +
              ".ctd{ border-style: solid;       border-width: 1px; border-color: #C0C0C0;  width: 100%; border-collapse: collapse; }"

            });
            Sections.Add(new Section
            {
                Text = "<br/><p><b>Pólizas anteriores:</b></p>" +
                "<table cellpadding='5' class='ctable'>" +
                       string.Join("", tblPolAnteriores.ToArray())
                       +
              "</table>"
            ,
                CSSFont = "table { font-family: Arial; font-size: 10px; }" +
                ".ctable{  border-style: solid; border-width: 1px; border-color: #C0C0C0;  width: 100%; border-collapse: collapse; }" +
                ".ctr{ border-style: solid;     border-width: 1px; border-color: #C0C0C0;  width: 100%; border-collapse: collapse; }" +
              ".ctd{ border-style: solid;       border-width: 1px; border-color: #C0C0C0;  width: 100%; border-collapse: collapse; }" +
              "p { font-family: Arial; font-size: 10px; text-align: justify; }"

            });
            Sections.Add(new Section
            {
                Text = "<br/><table cellpadding='5' class='ctable'>" +
                                   "<tr class='ctr'>" +
                                   "<td class='ctd' style='width: 20%;'><p><b>Coberturas:</b></p></td> " +
                                   "<td class='ctd' style='width: 60%;'>" +
                                   "<p>" +
                                         "<ul style='list-style-type:Disc'> " +
                                         "<li><b>Responsabilidad Civil Profesional.</b></li>" +
                                         "<li><b>Responsabilidad Civil por el uso de objetos peligrosos (objetiva)</b></li>" +
                                         "<li><b>Suministros de medicamentos y materiales de curación</b></li>" +
                                         "<li><b>Responsabilidad Civil por sus inmuebles y Responsabilidad Civil por sus Actividades del consultorio(os) que ocupe para ejercer su profesión.</ b></li>" +
                                         "<li><b>Cobertura automáticamente extendida para la responsabilidad civil y para la responsabilidad civil y profesional de su personal médico como empleados, de acuerdo a las condiciones generales.</ b></li>" +
                                         "</ul>" +
                                   "</p>" +
                                   "</td>" +
                                   "</tr>" +
                                   "<tr class='ctr'>" +
                                   "<td class='ctd' style='width: 20%;'><p><b>Coberturas Especiales sin costo:</b></p></td> " +
                                   "<td class='ctd' style='width: 60%;'>" +
                                   "<p>" +
                                   "Para las siguientes coberturas, están aseguradas, dentro del marco de las condiciones de la póliza, la responsabilidad civil legal en que incurriere el Asegurado por daños a terceros, derivada de las siguientes actividades que enseguida se indican:" +
                                   "</p> <br/>" +
                                   "<p>" +
                                   "-Responsabilidad civil del arrendatario de consultorio." +
                                   "</p>" +
                                   "<p>" +
                                   "Está asegurada la responsabilidad civil legal por daños que, por incendio y rayo o explosión, se causen al inmueble o inmuebles que el Asegurado haya tomado, totalmente o en parte, en arrendamiento, para ser usado por consultorio, siempre que dichos daños le sean imputables." +
                                   "</p> <br/>" +
                                   "<p>" +
                                   "- Responsabilidad civil como condómino de consultorio." +
                                   "</p>" +
                                   "<p>Está asegurada además la responsabilidad civil legal del Asegurado por daños ocasionados a las áreas comunes del condómino en el cual tenga su consultorio; además por daños ocasionados a los condóminos (no incluye las áreas del Asegurado) a consecuencia de un derrame de agua accidental e imprevisto.</p><br/>" +
                                   "<p>Sin embargo, para la indemnización a pagar por GMX Seguros se descontará un porcentaje, equivalente a la cuota del Asegurado como propietario de dichas áreas comunes.</p><br/>" +
                                   "<p>" +
                                   "- Responsabilidad civil privada y familiar." +
                                   "</p>" +
                                   "<p>La Responsabilidad Civil Legal en que incurra el Asegurado por daños a terceros, derivada de las actividades privadas y familiares, en cualquiera de los siguientes supuestos:</p><br/>" +
                                   "<p>" +
                                        "<ol style='list-style-type: lower-alpha; line-height: 130%;'>" +
                                        "<li>Como jefe de familia.</li>" +
                                        "<li>Como propietario de una o varias casas habitación, (incluye las habitadas en fines de semana y en vacaciones).</li>" +
                                        "<li>Por daños ocasionados a consecuencia de incendio, o explosión del inmueble, edificio u hogar.</li>" +
                                        "<li>Por daños a consecuencia de un derrame de agua accidental e imprevisto.</li>" +
                                        "<li>Por práctica de deportes como aficionado.</li>" +
                                        "<li>Por el uso de bicicletas, patines, embarcaciones de pedal o de remo y vehículos no motorizados.</li>" +
                                        "<li>Como propietario de animales domésticos, de caza y Guardianes.</li>" +
                                        "<li>Durante viajes de estudios, de vacaciones o de placer del Asegurado o algún dependiente económico dentro de la República Mexicana.</li>" +
                                        "<li>Durante viajes de estudios, de vacaciones o de placer del Asegurado o algún dependiente económico, en el extranjero. </li>" +
                                        "<li>Se cubre la responsabilidad que resulte ante los trabajadores domésticos por riesgo de trabajo, otorgándose las prestaciones marcadas en el Título IX de la Ley Federal del Trabajo, hasta por un límite de mil DSMGVDF por cada trabajador, máximo dos trabajadores. </li>" +
                                        "<li>Responsabilidad civil como condómino, está asegurada además la responsabilidad civil legal del Asegurado por daños ocasionados a las áreas comunes del condómino en el cual tenga su consultorio; sin embargo, la indemnización a pagar por GMX Seguros se descontará un porcentaje, equivalente a la cuota del Asegurado como propietario de dichas áreas comunes." +
                                        "</li>" +
                                        "</ol></p> <br/><p>" +
                                            "<ul style='list-style-type: disc; line-height: 130%;'>" +
                                                "<li>El límite máximo de responsabilidad opera como límite único y combinado, en el agregado anual., incluso sí posee más de un domicilio.</li>" +
                                                "<li>Límite de Responsabilidad Único y Máximo en el agregado anual incluyendo Gastos de Defensa Jurídica</li>" +
                                            "</ul>" +
                                    "</p>" +
                                   "</td>" +
                                   "</tr>" +

                                   "<tr class='ctr'>" +
                                   "<td class='ctd' style='width: 20%;'><p><b>Límite Máximo de Responsabilidad:</b></p></td> " +
                                   "<td class='ctd' style='width: 60%;'>" +
                                   "<p>" +
                                   "<ul style='list-style: disc; line-height: 130%;'>" +
                                        "<li>   Límite de Responsabilidad Único y Máximo en el agregado anual incluyendo Gastos de Defensa Jurídica: " + amount + "</li>" +
                                   "</ul>" +
                                   "<br/><p><b>Sublímite por Concepto de Daño Moral:</b> hasta el 50% del límite máximo de responsabilidad por evento y en el agregado anual contratado en la presente póliza.</p>" +
                                   "</p>" +
                                   "</td>" +
                                   "</tr>" +

                                   "<tr class='ctr'>" +
                                   "<td class='ctd' style='width: 20%;'><p><b>Sublímites:</b></p></td> " +
                                   "<td class='ctd' style='width: 60%;'>" +
                                   "<p><b>Responsabilidad Civil privada y familiar $ 100,000.00 M.N.</b></p><br/>" +
                                   "</td>" +

                                   "</tr>" +

                                   "<tr class='ctr'>" +
                                   "<td class='ctd' style='width: 20%;'><p><b>Deducibles:</b></p></td> " +
                                   "<td class='ctd' style='width: 60%;'>" +
                                   "<p><b>Responsabilidad Civil profesional</b></p><br/>" +
                                   "<p>-    Para daños a terceros en sus personas: Sin deducible.</p>" +
                                   "<p>-    Para daños a terceros en sus propiedades: Sin deducible</p>" +

                                   "<br/><p><b>Responsabilidad Civil privada y familiar</b></p><br/>" +
                                   "<p>-    Responsabilidad Civil en el extranjero: 10% del monto de cada reclamación, con mínimo " + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + " de $3,000.00 M.N.</p>" +
                                   "<p>-    Responsabilidad Civil en la República Mexicana: 10% del monto de cada reclamación, con mínimo de $2,000.00 M.N.</p>" +


                                   "<br/><p><b>Demás riesgos</b></p><br/>" +
                                   "<p>5% del monto de cada reclamación.</p>" +
                                   "</td>" +

                                   "</tr>" +

                                    "<tr  class='ctr'>" +
                                        "<td class='ctd'style='width: 20%;'><p><b>Riesgo Asegurado</b></p></td> " +
                                        "<td class='ctd'style='width: 60%;'> " +
                                            "<p>GMX Seguros se obliga a pagar la indemnización que el Asegurado deba a sus pacientes o a terceros dañados a consecuencia de uno o más hechos realizados sin dolo, ya sea por culpa negligente o imperita (acciones u omisiones), <b>o por el uso de cosas peligrosas, en la prestación de Servicios de Atención Médica.</b></p><br/>" +
                                            "<p>También queda incluida dentro de esta última la responsabilidad por el uso de mecanismos, instrumentos, aparatos o substancias peligrosas por sí mismos y que dan lugar a responsabilidad civil. Estos aparatos pueden ser todos los usados para fines del diagnóstico y de la terapéutica, en cuanto estén reconocidos por la ciencia médica, y causen un daño previsto en esta póliza a terceras personas con motivo de la prestación de servicios para la salud.</p><br/>" +
                                            "<p>Los daños amparados bajo la cobertura de responsabilidad civil comprenden: lesiones corporales, enfermedades, muerte, así como el deterioro o destrucción de bienes. Los perjuicios que resulten y el daño moral, <b>el cual se otorgará bajo un sublímite de 50% del límite máximo de responsabilidad contratado en la presente póliza;</b> los cuales sólo se cubren cuando sean consecuencia directa e inmediata de los citados daños y no como una agravante calificada por un juzgador, con las cuales haya actuado para la realización del daño, incluso cuando dichas agravantes sean considerados como parte de una indemnización identificada bajo el rubro o  el concepto de daño moral.</p><br/>" +
                                        "</td> " +
                                    "</tr> " +

                                   "<tr class='ctr'>" +
                                   "<td class='ctd' style='width: 20%;'><p><b>Base de Indemnización:</b></p></td> " +
                                   "<td class='ctd' style='width: 60%;'>" +
                                   "<p style='text-align: justify;'>Se aclara además que en este caso las disposiciones del Preámbulo y de la letra b) de la Cláusula 1ª del Capítulo I de la póliza (condiciones generales) se modifican para quedar como sigue:</p>" +
                                   "<br/><p style='text-align: justify;'>“Preámbulo:</p>" +
                                   "<br/><p style='text-align: justify;'>El presente contrato de seguro se celebra conforme a lo dispuesto en el inciso a) del Art. 145 bis de la Ley sobre el Contrato de Seguros, para cubrir la indemnización que el Asegurado deba a un tercero por hechos realizados desde la fecha convencional, siempre que la reclamación se formule por primera vez y por escrito al Asegurado o a GMX Seguros, en el curso de la vigencia de la presente póliza”</p>" +

                                   "<br/><p style='text-align: justify;'>Capítulo I, Cláusula 1ª:</p>" +
                                   "<br/><p style='text-align: justify;'>“b) Base de indemnización.</p>" +

                                   "<br/><p style='text-align: justify;'>El presente seguro cubre la indemnización que el Asegurado deba a un tercero, conforme a las condiciones pactadas en el presente contrato, por hechos realizados desde la fecha convencional, siempre que la reclamación se formule por primera vez y por escrito al Asegurado o a GMX Seguros, en el curso de la vigencia de la  presente póliza”</p>" +

                                   "<br/><p style='text-align: justify;'>Fecha convencional </p>" +

                                   "<br/><p style='text-align: justify;'>De acuerdo con lo previsto en el art. 145 bis de la Ley sobre el Contrato de Seguro, se otorga fecha convencional desde el inicio de vigencia de la primera póliza contratada con GMX Seguros, siempre que las renovaciones hayan sido una cadena ininterrumpida de seguros con GMX Seguros, ello sobre hechos no conocidos ni reclamados previamente al Asegurado o a GMX Seguros y siempre que la reclamación se formule por primera vez y por escrito al Asegurado o a GMX Seguros, durante la vigencia actual de la presente póliza.</p>" +
                                   "<br/><p style='text-align: justify;'>Se aclara que en caso de reclamación, aplicarán los límites y las condiciones que prevalecen en la póliza que corresponde al año del hecho generador que propicia la reclamación, por lo anterior, las obligaciones a cargo de GMX Seguros, se sujetarán al límite máximo  de responsabilidad y condiciones contratadas para la vigencia en que haya realizado el hecho generador del daño.</p>" +
                                   "</td>" +
                                   "</tr>" +

                #region Funciones del seguro
                                    //"<tr class='ctr'>" +
                                    //"<td class='ctd' style='width: 20%;'><p><b>Funciones del seguro:</b></p></td> " +
                                    //"<td class='ctd' style='width: 60%;'>" +
                                    // "<ul style='list-style-type: square;  line-height: 130%;'>" +
                                    //     "<li>Función indemnizatoria, que deba el asegurado a un paciente derivado de una negligencia, impericia o el uso de sustancias, aparatos u objetos que por su propia naturaleza causen un daño al paciente, así como el daño moral, siempre y cuando sea a consecuencia directa e inmediata del daño causado.</li>" +
                                    //     "<li>Función de análisis y defensa legal, reclamaciones judiciales y extrajudiciales, hechas al asegurado directamente o por medio de autoridades Administrativas (CONAMED, OIC, Derechos Humanos, Etc.), Juzgados Civiles (Materia Civil), Ministerios Públicos y Juzgados de lo Penal, local o federal (Materia Penal).</li>" +
                                    // "</ul>" +
                                    ////"</p>" +
                                    //"</td>" +
                                    //"</tr>" +
                #endregion

                                    "<tr class='ctr'> " +
                                        "<td class='ctd' style='width: 20%;'><p><b>Funciones del seguro</b></p></td> " +
                                        "<td class='ctd' style='width: 80%;'>" +
                                                                       "<p><b>Función Indemnizatoria</b></p>" +
                                                                       "<p>Que deba el Asegurado a su paciente derivado de una negligencia, impericia o el uso de sustancias, aparatos u objetos que por su propia naturaleza causen un daño al mismo, así como el de perjuicios y daño moral, siempre y cuando sea a consecuencia directa e inmediata del daño causado.</p><br/>" +
                                                                       "<p>Los daños comprenden lesiones corporales, enfermedades, muerte, así como el deterioro o destrucción de bienes.</p><br/>" +

                                                                       "<p><b>Función de Análisis y defensa legal</b></p>" +
                                                                       "<p>Queda a cargo de GMX Seguros y dentro del límite de responsabilidad asegurado en esta póliza el pago de los gastos de defensa legal del Asegurado.</p><br/>" +
                                                                       "<p>Dichos gastos incluyen la tramitación judicial, así como el análisis de las reclamaciones de terceros, aun cuando ellas sean infundadas, y las cauciones y primas de fianzas requeridas procesalmente.</p><br/>" +

                                                                       "<p><b>*** Se entenderá que la función de análisis y defensa jurídica corresponde única y exclusivamente a GMX Seguros y operará bajo los lineamentos que establezca en su dirección y control del siniestro.</b></p>" +
                                        "</td>" +
                                    "</tr> " +


                                   "<tr class='ctr'>" +
                                   "<td class='ctd' style='width: 20%;'><p><b>Exclusiones:</b></p></td> " +
                                   "<td class='ctd' style='width: 60%;'>" +
                                   "<br/><p>" +
                                    "<ul style='list-style-type: square;  line-height: 130%;'>" +
                                        "<li><b>No será efectiva esta cobertura si el Asegurado no acredita su relación o pertenencia como médico de Grupo Angeles</b></li>" +
                                        "<li>Acciones dolosas de parte del asegurado</li>" +
                                        "<li>Garantía de calidad del servicio en cuanto a su cumplimiento en tiempo y forma</li>" +
                                        "<li>Incumplimiento de contratos</li>" +
                                        "<li>Multas, daños punitivos o ejemplares y/o venganza</li>" +
                                        "<li>Responsabilidad por la fabricación de productos farmacéuticos y transgénicos además la participación en estudios farmacológicos o de nuevos recursos.</li>" +
                                        "<li>R.C. Patronal</li>" +
                                        "<li>R.C. Estacionamiento</li>" +
                                    "</ul>" +
                                   "</p>" +
                                   "</td>" +
                                   "</tr>" +

                                   //"<tr class='ctr'>" +
                                   //"<td class='ctd' style='width: 20%;'><p><b>Condiciones especiales:</b></p></td> " +
                                   //"<td class='ctd' style='width: 60%;'>" +
                                   //"<p style='text-align: justify;'>Para las siguientes coberturas, están aseguradas, dentro del marco de las condiciones de la póliza, la responsabilidad civil legal en que incurriere el Asegurado por daños a terceros, derivada de las siguientes actividades que enseguida se indican:</p>" +
                                   //"<br/><p style='text-align: justify;'><b>Responsabilidad civil del arrendatario de consultorio.</b></p>" +
                                   //"<p style='text-align: justify;'>Está asegurada la responsabilidad civil legal por daños que, por incendio y rayo o explosión, se causen al inmueble o inmuebles que el Asegurado haya tomado, totalmente o en parte, en arrendamiento, para ser usado por consultorio, siempre que dichos daños le sean imputables.</p>" +
                                   //"<br/><p style='text-align: justify;'><b>Responsabilidad civil como condómino de consultorio.</b></p>" +
                                   //"<p style='text-align: justify;'>Está asegurada además la responsabilidad civil legal del Asegurado por daños ocasionados a las áreas comunes del condómino en el cual tenga su consultorio; además por daños ocasionados a los condóminos (no incluye las áreas del Asegurado) a consecuencia de un derrame de agua accidental e imprevisto.</p>" +

                                   //"<br/><p style='text-align: justify;'>Sin embargo, para la indemnización a pagar por GMX Seguros se descontará un porcentaje, equivalente a la cuota del Asegurado como propietario de dichas áreas comunes. </p>" +

                                   //"<br/><p style='text-align: justify;'><b>Responsabilidad civil privada y familiar.</b></p>" +
                                   //"<p style='text-align: justify;'>La Responsabilidad Civil Legal en que incurra el Asegurado por daños a terceros, derivada de las actividades privadas y familiares, en cualquiera de los siguientes supuestos:</p>" +

                                   //"</td>" +
                                   //"</tr>" +

                                   "<tr class='ctr'>" +
                                   "<td class='ctd' style='width: 20%;'><p><b>Territorialidad y Jurisdicción:</b></p></td> " +
                                   "<td class='ctd' style='width: 60%;'>" +
                                    "<p>" +
                                        "La responsabilidad civil materia del seguro se determina conforme a la legislación vigente en los Estados Unidos Mexicanos." +
                                   "</p>" +
                                   "</td>" +
                                   "</tr>" +


                              "</table>",
                CSSFont = "table { font-family: Arial; font-size: 10px; }" +
                            ".ctable{  border-style: solid; border-width: 1px; border-color: #C0C0C0;  width: 100%; border-collapse: collapse; }" +
                            ".ctr{ border-style: solid;     border-width: 1px; border-color: #C0C0C0;  width: 100%; border-collapse: collapse; }" +
                          ".ctd{ border-style: solid;       border-width: 1px; border-color: #C0C0C0;  width: 100%; border-collapse: collapse; }"
            });
            Sections.Add(new Section
            {
                Text = "<br/><p><b>- Condiciones Especiales:</b></p><br/>" +
            "<p>-Médico Sustituto</p><br/>" +
            "<p>Se especifica que en referencia a la cobertura de sustitución provisional mencionado  en el Condicionado General de la presente póliza en el Capítulo III, clausula única de Responsabilidad Civil Profesional, punto 4, inciso b,  se especifica lo siguiente:</p><br/>" +
            "<p>b) El presente seguro se amplía a cubrir la responsabilidad cuando al Asegurado, por ausencias temporales encargue a un médico sustituto, de su misma especialidad, la atención de sus pacientes.</p><br/>" +
            "<p>Dicha condición aplicará siempre y cuando la ausencia de dicho asegurado se origine por su participación en congresos, vacaciones o motivos de fuerza mayor y el paciente tenga el pleno conocimiento y de su consentimiento de ser atendido por el médico sustituto; no será materia de cobertura , en caso de que por cualquier circunstancia, se tome la determinación de realizar algún procedimiento quirúrgico  que no cuente con los puntos mencionados anteriormente, en estos casos la compañía aseguradora  no tendrá ninguna responsabilidad, ni hará frente a la  reclamación derivada de dicha circunstancia.</p><br/>" +
            "<p><b>“VALOR AGREGADO “Plan de asistencia legal”:</b></p><br/>" +
            "<p><b>Como parte de la cobertura queda incluido el valor agregado denominado “Plan de asistencia legal”, bajo los términos y condiciones particulares y folleto que se anexan a la presente póliza, se aclara que este servicio es adicional e independiente a la cobertura de la presente póliza, por lo tanto no afectará la suma asegurada contratada, lo anterior en caso de que sea utilizada por el asegurado.”:</b></p>",
                CSSFont = "p { font-family: Arial; font-size: 10px; text-align: justify; }"
            });
        }
        #endregion Angeles


        #region Cotizacion
        public static void GetSlipCotizacionAng(VMCotizar vm)
        {
            string tblLimites = "";
            wsbd.Service ws = new wsbd.Service(config.Config["APIBD"]);
            string json = ws.get_catalogos("GetAllSumaAseg_Angeles", "");
            ListaSumaAngeles lstangeles = JsonConvert.DeserializeObject<ListaSumaAngeles>(json);
            int num = 0;
            foreach (SumaAsegAngeles s in lstangeles.Table)
            {
                tblLimites += "<tr> ";
                tblLimites += "<td class='ctd'><p> " + (num += 1) + ". " + $"{s.SumaAsegurada.ToString("c")} M.N." + "</p></td>";
                tblLimites += "<td class='ctd'><p>" + $"{s.PrimaUnica.ToString("c")} M.N." + "</p></td>";
                tblLimites += "</tr>";
            }

            docPDF = new DocumentPDF();
            Sections = new List<Section>();

            docPDF.IsCotizacion = true;
            string Header = "<center><table><tr><td><br/><h1>COTIZACIÓN</h1><h2>Seguro de Responsabilidad Civil Profesional Para Profesiones Médicas, Auxiliares y Técnicas</h2><br/><br/></td></tr></table></center><br/><br/>";
            docPDF.StaticText = Header;
            docPDF.StaticCSS = "h1{ font-family: 'Arial'; font-size: 20; text-align: center; color: #9c9c9c; } h2{ font-family: Arial; font-size: 13; text-align: center; color: #9c9c9c; } table{border: 0px solid #d0d0d0;  margin: auto; width:100 %; }";
            docPDF.StaticXPosition = 64;//110;
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

            CultureInfo ci = new CultureInfo("es-MX");
            string Today = $"México, D.F. a {DateTime.Now.ToString("dd 'de' MMMM 'de' yyyy", ci)}"; //String.Format("{0:d}", DateTime.Today);
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
                Text = "<table style='width: 100%;'><tr><td style='text-align: right'><p>Fecha de expedición: " + Today + "</p></td></tr></table><br/>"
               ,
                CSSFont = "p{ font-size: 12; font-family : Arial; text-align: right;}"
            });
            #endregion
            #region BENEFICIO Dirigido

            Sections.Add(new Section
            {
                Text = "<table style='width: 100%;'>"
                        + "<tr><td style='width: 500px;'></td><td style='text-align: left'><p><b>BENEFICIO Dirigido exclusivamente para Médicos pertenecientes a Grupo Ángeles (Nacional)</b></p></td></tr>"
                        + "<tr><td style='width: 500px;'></td><td style='text-align: left'><p><b>Ref.:Seguro GMX de RC, </b><i>Seguro de responsabilidad civil y responsabilidad profesional para las profesiones Médicas</i></p></td></tr>"
                        + "</table><br/>"
               ,
                CSSFont = "p{ font-size: 12; font-family : Arial; text-align: left;}"
            });
            #endregion
            #region BODY
            Sections.Add(new Section
            {
                Text = "<h1 style='text-align:left;'><p><b>Estimado Médico que pertenece a Grupo Ángeles:</b></p></h1><br/>" +
                        "<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;GMX Seguros es una aseguradora 100% mexicana, comprometida con los profesionales al cuidado de la salud, líder en México en seguros de responsabilidad civil y particularmente en responsabilidad civil profesional médica con más de 250, 000 Médicos Asegurados.</p><br/>" +
                        "<p>A continuación nos permitimos exponer las características y ventajas de nuestro producto:</p><br/>"
                        ,
                CSSFont = "h1{ font-size: 18; font-family : Arial; text-align: left;} p{ font-size: 12; font-family : Arial; text-align: justify;}"

            });
            Sections.Add(new Section
            {
                Text = "<ol class='a'>" +
                "<li value='1'><pre>Indemnizar al paciente afectado o algún tercero para cubrir los montos que deba el asegurado a un paciente derivado de un acto negligente o imperito o por el uso de sustancias, aparatos u objetos que por su propia naturaleza causen un daño, así como el pago del daño moral y el perjuicio.</pre></li>" +
                "<li>Defensa jurídica, reclamaciones judiciales y extrajudiciales hechas al Asegurado directamente o por medio de:" +
                "<ul style='font-family: Arial; font-size: 12px; list-style-type: disc; text-align: justify;'>" +
                "<li style='list-style-type: circle'>Autoridades Administrativas (<b>CONAMED</b>, Derechos Humanos, Órganos Internos de control, Función Pública, por mencionar algunos) </li>" +
                "<li style='list-style-type: circle'>Materia Civil (<b>Juzgados Civiles</b>)</li>" +
                "<li style='list-style-type: circle'>Materia Penal (<b>Ministerios Públicos y Juzgados Penales</b>). Todos los anteriores tanto federales como del fuero común,</li>" +
                "</ul></li></ol>"
                ,
                CSSFont = "ol.a {font-family: Arial; font-size: 12; list-style-type: decimal; text-align: justify;} p{ font-size: 12; font-family : Arial; text-align: justify;}"
            });
            Sections.Add(new Section
            {
                Text = "<br/><p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;En esta cobertura se le asigna abogado especialista, de acuerdo al caso en concreto y al lugar donde se le esté reclamando, y el muy importante pago, de primas de <b>fianzas y las cauciones en efectivo.</b></p><br/>"
                        + "<p><b>Beneficios:</b></p>"
                        + "<ul style='font-family: Arial; font-size: 12px; list-style-type: disc; text-align: justify;'>"
                        + "<li style='list-style-type: circle'>Los Asegurados de GMX Seguros, tendrán cobertura retroactiva desde la primera póliza que se contrató con nosotros (fecha convencional), sin perder sus derechos.</li>"
                        + "<li style='list-style-type: circle'>Nuestra cobertura básica es: <b>SIN DEDUCIBLE</b></li>"
                        + "<li style='list-style-type: circle'>La atención de siniestros, de alta especialización con una trayectoria única en el mercado mexicano, por la<b> experiencia de más 250 mil médicos asegurados.</b></li>"
                        + "</ul>"
                ,
                CSSFont = "p{ font-size: 12; font-family : Arial; text-align: justify;}"

            });
            Sections.Add(new Section { onlyNewPage = true });
            Sections.Add(new Section
            {
                Text = "<p><b>Cobertura*:</b></p><ol class='a'>" +
                "<li>Responsabilidad Civil Profesional Médica por el ejercicio de su profesión, por alguna reclamación o demanda de parte de sus pacientes derivado de su ejercicio profesional.</li>" +
                "<li>Responsabilidad Civil por sus inmuebles y por sus actividades, por ejemplo:" +
                "<ul style='font-family: Arial; font-size: 12px; list-style-type: square; text-align: justify;'>" +
                "<li>Daños causados a los pacientes o a terceros derivados del uso de instalaciones higiénicas, y relacionadas con las terapias o rehabilitaciones.</li>" +
                "</ul></li>"
                + "<li>Responsabilidad Civil como arrendatario y condómino, en caso de que rente su consultorio.</li>"
                + "<li>Responsabilidad Civil Privada y Familiar, hasta un Sublímite de $100,000.00 MN </li>"
                + "</ol>"
                ,
                CSSFont = "ol.a {font-family: Arial; font-size: 12; list-style-type: disc; text-align: justify;}  p{ font-size: 12; font-family : Arial; text-align: justify;}"
            });
            Sections.Add(new Section
            {
                Text = "<br/><table cellpadding='5' class='ctable'><tr class='ctr'><td class='ctd'><p><b>Límite de responsabilidad:</b></p></td><td class='ctd'><p><b>Prima NETA:</b></p></td></tr> " + tblLimites + "  </table>"
                        + "<p><b>Nota: A las primas anteriores se debe de agregar los derechos de póliza ($350.00 M.N.) e IVA correspondiente.</b></p><br/>"
                        + "<p><b>Se especifica que la entrada en vigor del presente comunicado es a partir del 04 de Julio de 2016.</b></p><br/>"
                        + "<p><i>De acuerdo a los términos, condiciones particulares y generales del texto de GMX Seguro, definidos para este proyecto.</i></p>"
                ,
                CSSFont = "table { font-family: Arial; font-size: 10px; } p{ font-size: 12; font-family : Arial; text-align: justify;}" +
                            ".ctable{  border-style: solid; border-width: 1px; border-color: #C0C0C0;  width: 100%; border-collapse: collapse; }" +
                            ".ctr{ border-style: solid;     border-width: 1px; border-color: #C0C0C0;  width: 100%; border-collapse: collapse; }" +
                          ".ctd{ border-style: solid;       border-width: 1px; border-color: #C0C0C0;  width: 100%; border-collapse: collapse; }"
            });
            #endregion

        }

        public static void GetSlipCotizacionTrad(VMCotizar vm)
        {
            docPDF = new DocumentPDF();
            Sections = new List<Section>();

            docPDF.IsCotizacion = true;
            string Header = "<center><table><tr><td><br/><h1>COTIZACIÓN</h1><h2>Seguro de Responsabilidad Civil Profesional Para Profesiones Médicas, Auxiliares y Técnicas</h2><br/><br/></td></tr></table></center><br/><br/>";
            docPDF.StaticText = Header;
            docPDF.StaticCSS = "h1{ font-family: 'Arial'; font-size: 20; text-align: center; color: #9c9c9c; } h2{ font-family: Arial; font-size: 13; text-align: center; color: #9c9c9c; } table{border: 0px solid #d0d0d0;  margin: auto; width:100 %; }";
            docPDF.StaticXPosition = 64;//110;
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

            CultureInfo ci = new CultureInfo("es-MX");
            string Today = $"México, D.F. a {DateTime.Now.ToString("dd 'de' MMMM 'de' yyyy", ci)}"; //String.Format("{0:d}", DateTime.Today);
            string SumaAseg = $"{vm.SumaAseg} M.N.";
            string pNeta = $"{vm.PrimaNeta.ToString("c")} M.N.";
            string recargos = $"{0.ToString("c")} M.N.";
            string derechos = $"{vm.Derechos.ToString("c")} M.N.";
            string sTotal = $"{vm.SubTotal.ToString("c")} M.N.";
            string iva = $"{vm.Iva.ToString("c")} M.N.";
            string pTotal = $"{vm.PrimaTotal.ToString("c")} M.N.";
            string cober_rcArrend = vm.Adicional ? "<li><b>Responsabilidad Civil Arrendatario</b></li>" : string.Empty;

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
                                       "<td class='ctd' style='width: 60%;'" + "><p>Límite Máximo de Responsabilidad Único y Máximo en el agregado anual incluyendo Gastos de Defensa Juridica: " + SumaAseg + "</p><br/>" +
                                       "<p><b>Sublímite por Concepto de Daño Moral:</b> hasta el 50% del límite máximo de responsabilidad por evento y en el agregado anual contratado en la presente póliza.</p>" +
                                       "</td>" +
                                   "</tr>" +
                                   "<tr>" +
                                   "<td class='ctd' style='width: 20%;'><p><b>Coberturas</b></p></td> " +
                                   "<td class='ctd' style='width: 60%;'>" +
                                   "<p>" +
                                         "<ul style='list-style-type:Disc'> " +
                                         "<li><b>Responsabilidad Civil Profesional.</b></li>" +
                                         "<li><b>Responsabilidad Civil por el uso de objetos peligrosos (objetiva)</b></li>" +
                                         "<li><b>Suministros de medicamentos y materiales de curación</b></li>" +
                    "<li><b>Responsabilidad Civil por sus inmuebles y Responsabilidad Civil por sus Actividades </b></li>" + cober_rcArrend +
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
                                            "<p>GMX Seguros se obliga a pagar la indemnización que el Asegurado deba a sus pacientes o a terceros dañados a consecuencia de uno o más hechos realizados sin dolo, ya sea por culpa negligente o imperita (acciones u omisiones), <b>o por el uso de cosas peligrosas, en la prestación de Servicios de Atención Médica.</b></p><br/>" +
                                            "<p>También queda incluida dentro de esta última la responsabilidad por el uso de mecanismos, instrumentos, aparatos o substancias peligrosas por sí mismos y que dan lugar a responsabilidad civil. Estos aparatos pueden ser todos los usados para fines del diagnóstico y de la terapéutica, en cuanto estén reconocidos por la ciencia médica, y causen un daño previsto en esta póliza a terceras personas con motivo de la prestación de servicios para la salud.</p><br/>" +
                                            "<p>Los daños amparados bajo la cobertura de responsabilidad civil comprenden: lesiones corporales, enfermedades, muerte, así como el deterioro o destrucción de bienes. Los perjuicios que resulten y el daño moral, <b>el cual se otorgará bajo un sublímite de 50% del límite máximo de responsabilidad contratado en la presente póliza;</b> los cuales sólo se cubren cuando sean consecuencia directa e inmediata de los citados daños y no como una agravante calificada por un juzgador, con las cuales haya actuado para la realización del daño, incluso cuando dichas agravantes sean considerados como parte de una indemnización identificada bajo el rubro o  el concepto de daño moral.</p><br/>" +
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
                                        "<td class='ctd' style='width: 20%;'><p><b>Funciones del seguro</b></p></td> " +
                                        "<td class='ctd' style='width: 80%;'>" +
                                                                       "<p><b>Función Indemnizatoria</b></p>" +
                                                                       "<p>Que deba el Asegurado a su paciente derivado de una negligencia, impericia o el uso de sustancias, aparatos u objetos que por su propia naturaleza causen un daño al mismo, así como el de perjuicios y daño moral, siempre y cuando sea a consecuencia directa e inmediata del daño causado.</p><br/>" +
                                                                       "<p>Los daños comprenden lesiones corporales, enfermedades, muerte, así como el deterioro o destrucción de bienes.</p><br/>" +

                                                                       "<p><b>Función de Análisis y defensa legal</b></p>" +
                                                                       "<p>Queda a cargo de GMX Seguros y dentro del límite de responsabilidad asegurado en esta póliza el pago de los gastos de defensa legal del Asegurado.</p><br/>" +
                                                                       "<p>Dichos gastos incluyen la tramitación judicial, así como el análisis de las reclamaciones de terceros, aun cuando ellas sean infundadas, y las cauciones y primas de fianzas requeridas procesalmente.</p><br/>" +

                                                                       "<p><b>*** Se entenderá que la función de análisis y defensa jurídica corresponde única y exclusivamente a GMX Seguros y operará bajo los lineamentos que establezca en su dirección y control del siniestro.</b></p>" +
                                        "</td>" +
                                    "</tr> " +
                                    "<tr class='ctr'> " +
                                        "<td class='ctd' style='width: 20%;'><p><b>Territorialidad y Jurisdicción</b></p></td> " +
                                        "<td class='ctd' style='width: 80%;'>" +
                                                                       "<p>La responsabilidad civil materia del seguro se determina conforme a la legislación vigente en los Estados Unidos Mexicanos.</p><br/>" +
                                        "</td>" +
                                    "</tr> " +
                          //"<tr class='ctr'> " +
                          //    "<td class='ctd' style='width: 20%;'><p><b>Condiciones Especiales </b></p></td> " +
                          //    "<td class='ctd' style='width: 80%;'>" +
                          //                                   "<p>Todos los términos y condiciones conforme al texto de Seguro de responsabilidad civil y de responsabilidad civil profesional para profesiones médicas y sus profesiones auxiliares y técnicas.</p><br/>" +
                          //    "</td>" +
                          //"</tr> " +
                          "</table>"
                                   ,
                CSSFont = "table { font-family: Arial; font-size: 10px; }" +
                            ".ctable{  border-style: solid; border-width: 1px; border-color: #C0C0C0;  width: 100%; border-collapse: collapse; }" +
                            ".ctr{ border-style: solid;     border-width: 1px; border-color: #C0C0C0;  width: 100%; border-collapse: collapse; }" +
                          ".ctd{ border-style: solid;       border-width: 1px; border-color: #C0C0C0;  width: 100%; border-collapse: collapse; }"


            });
            #endregion
            Sections.Add(new Section
            {
                Text = "<br/><p><b>- Condiciones Especiales:</b></p><br/>" +
            "<p>-Médico Sustituto</p><br/>" +
            "<p>Se especifica que en referencia a la cobertura de sustitución provisional mencionado  en el Condicionado General de la presente póliza en el Capítulo III, clausula única de Responsabilidad Civil Profesional, punto 4, inciso b,  se especifica lo siguiente:</p><br/>" +
            "<p>b) El presente seguro se amplía a cubrir la responsabilidad cuando al Asegurado, por ausencias temporales encargue a un médico sustituto, de su misma especialidad, la atención de sus pacientes.</p><br/>" +
            "<p>Dicha condición aplicará siempre y cuando la ausencia de dicho asegurado se origine por su participación en congresos, vacaciones o motivos de fuerza mayor y el paciente tenga el pleno conocimiento y de su consentimiento de ser atendido por el médico sustituto; no será materia de cobertura , en caso de que por cualquier circunstancia, se tome la determinación de realizar algún procedimiento quirúrgico  que no cuente con los puntos mencionados anteriormente, en estos casos la compañía aseguradora  no tendrá ninguna responsabilidad, ni hará frente a la  reclamación derivada de dicha circunstancia.</p><br/>" +
            "<p><b>“VALOR AGREGADO “Plan de asistencia legal”:</b></p><br/>" +
            "<p><b>Como parte de la cobertura queda incluido el valor agregado denominado “Plan de asistencia legal”, bajo los términos y condiciones particulares y folleto que se anexan a la presente póliza, se aclara que este servicio es adicional e independiente a la cobertura de la presente póliza, por lo tanto no afectará la suma asegurada contratada, lo anterior en caso de que sea utilizada por el asegurado.”:</b></p>",
                CSSFont = "p { font-family: Arial; font-size: 10px; text-align: justify; }"
            });
        }
        #endregion Cotizacion
    }
}
