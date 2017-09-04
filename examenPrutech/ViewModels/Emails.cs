using System;
using GMXHelper;
using System.Collections.Generic;

namespace GMX.ViewModels
{
    public class Emails
    {
        public static DocumentPDF docPDF { get; set; }
        public static List<Section> Sections { get; set; }

		#region Tradicional
        public static void SlipTradicional(string numpoliza, bool rc, string nombrecliente, string desc, string esp, string cedprof, string cedesp, string diplo, string sumaaseg)
		{
			docPDF = new DocumentPDF();
			Sections = new List<Section>();

            string Header = "<center><table><tr><td><br/><h2>ESPECIFICACI”N QUE SE ADHIERE Y FORMA PARTE INTEGRANTE DE LA P”LIZA</h2><h2>" + numpoliza + "</h2><br/><br/></td></tr></table></center><br/><br/>";
			docPDF.StaticText = Header;
			docPDF.StaticCSS = "h2{ font-family: 'Segoe UI'; font-size: small; text-align: center; color: #9c9c9c; } h3{ font-family: 'Segoe UI'; font-size: xx-small; text-align: center; color: #9c9c9c; } table{border: 0px solid #d0d0d0;  margin: auto; width:100 %; }";

			docPDF.PVLID4CondGrales = "PVLM3D";
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

			Sections.Add(new Section
			{
				Text = "<table cellpadding='5' class='ctable'>" +
								   "<tr class='ctr'> " +
									   "<td class='ctd' style='width: 20%;'><p><b>Asegurado:</b></p></td> " +
                    "<td class='ctd' style='width: 60%;'><p>" + nombrecliente + "</p></td>" +
								   "</tr>" +
								   "<tr class='ctr'> " +
									   "<td class='ctd' style='width: 20%;'><p><b>EspecificaciÛn:</b></p></td> " +
                    "<td class='ctd' style='width: 60%;'><p>" + desc + "</p></td>" +
								   "</tr>" +
								   "<tr class='ctr'> " +
									   "<td class='ctd' style='width: 20%;'><p><b>Especialidad y/o Subespecialidad:</b></p></td> " +
                    "<td class='ctd' style='width: 60%;'><p>" + esp + "</p></td>" +
								   "</tr>" +
								   "<tr class='ctr'> " +
									   "<td class='ctd' style='width: 20%;'><p><b>CÈdula profesional:</b></p></td> " +
                    "<td class='ctd' style='width: 60%;'><p>" + cedprof + "</p></td>" +
								   "</tr>" +
								   "<tr class='ctr'> " +
									   "<td class='ctd' style='width: 20%;'><p><b>CÈdula de especialidad:</b></p></td> " +
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
									   "<td class='ctd' style='width: 20%;'><p><b>LÌmite M·ximo de Responsabilidad</b></p></td> " +
                    "<td class='ctd' style='width: 60%;'><p>LÌmite M·ximo de Responsabilidad ⁄nico y M·ximo en el agregado anual incluyendo Gastos de Defensa Juridica: " + sumaaseg + "</p></td>" +
								   "</tr>" +
								   "<tr class='ctr'>" +
								   "<td class='ctd' style='width: 20%;'><p><b>Coberturas</b></p></td> " +
								   "<td class='ctd' style='width: 60%;'>" +
								   "<p>" +
										 "<ul style='list-style-type:Disc'> " +
										 "<li><b>Responsabilidad Civil Profesional.</b></li>" +
										 "<li><b>Responsabilidad Civil por el uso de objetos peligrosos (objetiva)</b></li>" +
										 "<li><b>Suministros de medicamentos y materiales de curaciÛn</b></li>" +
										 "<li><b>Responsabilidad Civil por sus inmuebles y Responsabilidad Civil por sus Actividades </b></li>" +
										 "<li><b>Responsabilidad Civil Profesional MÈdica,</b> por la prestaciÛn de sus servicios de la salud, en toda la Rep˙blica Mexicana(Consultorios, torres mÈdicas, Hospitales P˙blicos y Privados, en general todo su ejercicio profesional)</li>" +
										 "<li><b>Responsabilidad Civil inmuebles,</b> derivada del uso y posesiÛn del inmueble de su consultorio.</li>" +
										 "<li><b>Responsabilidad Civil por sus actividades,</b> por ejemplo: Por el uso de piscinas, baÒos, gimnasios, aparatos de rehabilitaciÛn fÌsica, etc.relacionados con las terapias o rehabilitaciones.</li>" +
										 "<li><b>Responsabilidad por el uso de objetos peligrosos</b> (ìobjetivaî). Responsabilidad por el uso de los mecanismos, instrumentos, aparatos o substancias peligrosos por sÌ mismos y que dan lugar a responsabilidad civil. Estos aparatos pueden ser todos los usados para fines del diagnÛstico o de la terapÈutica, en cuanto estÈn reconocidos por la ciencia mÈdica incluyendo el uso de f·rmacos y materiales de curaciÛn.</li>" +
										 "<li><b>AmpliaciÛn a empleados y trabajadores del asegurado.</b> El presente seguro se amplÌa a cubrir la responsabilidad profesional legal de sus empleados y trabajadores en el desempeÒo de sus funciones derivadas de la actividad materia de este seguro, que se indica en la cÈdula de esta pÛliza. Para los efectos de esta pÛliza el m·ximo de empleados, bajo relaciÛn de trabajo, que cubre este seguro es de hasta, en consideraciÛn del ejercicio privado de un profesionista mÈdico:<br/>" +
												"<p>&nbsp;&nbsp;&nbsp;&nbsp;ï 2 secretarias o enfermeras-secretarias</p><br/>" +
												"<p>&nbsp;&nbsp;&nbsp;&nbsp;ï 2 mÈdicos auxiliares</p><br/>" +
												"<p>&nbsp;&nbsp;&nbsp;&nbsp;ï 3 enfermeras</p><br/>" +
										 "</li>" +
										 "<li><b>Medico sustituto.</b>TambiÈn el presente seguro se amplÌa a cubrir la responsabilidad cuando al Asegurado, por ausencias temporales encargue a un mÈdico sustituto, de su misma especialidad, la atenciÛn de sus pacientes.</li>" +
										 "<li>Esta cobertura se amplÌa, en su caso, para <b>herederos legales y cÛnyuges,</b> en caso de fallecimiento del asegurado y que se interponga reclamaciÛn a la masa hereditaria o a la sociedad conyugal respectivamente.</li>" +
										 "<li><b>Plan de asistencia Legal,</b> adicional y sin costo, anexo condiciones particulares.</li>" + cober_rcArrend +
										 "</ul>" +
								   "</p>" +
								   "</td>" +
								   "</tr>" +

								   "<tr class='ctr'> " +
									   "<td class='ctd' style='width: 20%;'><p><b>Riesgo Asegurado:</b></p></td> " +
									   "<td class='ctd' style='width: 60%;'>" +
									   "<p>Seguro de responsabilidad civil y de responsabilidad civil profesional para profesiones mÈdicas y sus profesiones auxiliares y tÈcnicas:</p><br/>" +
									   "<p>GMX Seguros se obliga a pagar la indemnizaciÛn que el Asegurado deba a sus pacientes o a terceros daÒados a consecuencia de uno o m·s hechos realizados sin dolo, ya sea por culpa negligente o imperita (acciones u omisiones), que ocasione en el ejercicio de las profesiones mÈdicas o de las profesiones tÈcnicas o auxiliares de la medicina o por el uso de cosas peligrosas.</p><br/>" +
									   "<p>TambiÈn queda incluida dentro de esta ˙ltima la responsabilidad por el uso de mecanismos, instrumentos, aparatos o substancias peligrosas por sÌ mismos y que dan lugar a responsabilidad civil.</p><br/>" +
									   "<p>Estos aparatos pueden ser todos los usados para fines del diagnÛstico y de la terapÈutica, en cuanto estÈn reconocidos por la ciencia mÈdica, y causen un daÒo previsto en esta pÛliza a terceras personas con motivo de la prestaciÛn de servicios para la salud.</p><br/>" +
									   "<p style='text-align: justify;'>No ser·n materia de cobertura las indemnizaciones adicionales a las que sea condenado el Asegurado por las agravantes con las cuales haya actuado para la realizaciÛn del daÒo, incluso cuando dichas agravantes sean considerados como parte de una indemnizaciÛn identificada bajo el rubro o el concepto de daÒo moral.</p><br/>" +
									   "<p>Para la cobertura de Responsabilidad Civil los daÒos comprenden: lesiones corporales, enfermedades, muerte; asÌ como el deterioro o destrucciÛn de bienes de terceras personas. Los perjuicios que resulten y el daÒo moral sÛlo se cubren cuando sean consecuencia directa e inmediata de los citados daÒos, causados a sus pacientes o a terceros daÒados.</p>" +
									   "</td>" +
								   "</tr>" +

								   "<tr class='ctr'> " +
										"<td class='ctd' style='width: 20%;'><p><b>Base de IndemnizaciÛn integrantes nuevos:</b></p></td> " +
										"<td class='ctd' style='width: 60%;'>" +
											"<p>Conforme a lo dispuesto en el Art. 145 bis de la ley sobre el Contrato de Seguro, el presente seguro cubre la indemnizaciÛn que el Asegurado deba a un tercero, conforme a las condiciones pactadas en el presente contrato, por  hechos ocurridos durante la vigencia de la pÛliza, siempre que la reclamaciÛn se formule por primera vez y por escrito al Asegurado o a GMX Seguros, en el curso de la vigencia de esta pÛliza o dentro del siguiente aÒo a su terminaciÛn.</p>" +
										"</td>" +
								   "</tr>" +

								   "<tr class='ctr'>" +
								   "<td class='ctd' style='width: 20%;'><p><b>Exclusiones:</b></p></td> " +
								   "<td class='ctd' style='width: 60%;'>" +
								   "<p>" +
										 "<ul style='list-style-type:Disc'> " +
											"<li><b>Acciones dolosas de parte del asegurado</b></li>" +
											"<li><b>GarantÌa de calidad del servicio.</b></li>" +
											"<li><b>Incumplimiento de contratos.</b></li>" +
											"<li><b>Obligaciones derivadas de un contrato.</b></li>" +
											"<li><b>R.C. Productos, incluyendo dentro de Èstos los farmacÈuticos y transgÈnicos.</b></li>" +
											"<li><b>R.C. Patronal.</b></li>" +
											"<li><b>R.C. Contractual.</b></li>" +
											"<li><b>Dem·s que seÒalan las condiciones generales</b></li>" +
										 "</ul>" +
								   "</p>" +
								   "</td>" +
								   "</tr>" +

								   "<tr class='ctr'>" +
								   "<td class='ctd' style='width: 20%;'><p><b>Deducibles:</b></p></td> " +
								   "<td class='ctd' style='width: 60%;'>" +
								   "<p>" +
										 "<ul style='list-style-type:Disc'> " +
											"<li>Para daÒos a terceros en sus personas: Sin deducible.</li>" +
											"<li>Para daÒos a terceros en sus propiedades: Sin deducible.</li>" +
										 "</ul>" +
								   "</p>" +
								   "</td>" +
								   "</tr>" +

								   "<tr class='ctr'>" +
								   "<td class='ctd' style='width: 20%;'><p><b>Funciones del seguro:</b></p></td> " +
								   "<td class='ctd' style='width: 60%;'>" +
								   "<p>" +
										 "<ul style='list-style-type:Disc'> " +
											"<li><b>FunciÛn indemnizatoria,</b> que deba el asegurado a su paciente derivado de una negligencia, impericia o el uso de sustancias, aparatos u objetos que por su propia naturaleza causen un daÒo al mismo, asÌ como el los perjuicios y daÒo moral, siempre y cuando sea a consecuencia directa e inmediata del daÒo causado.</li>" +
										 "</ul>" +
										 "<br/><br/><p>Los daÒos comprenden lesiones corporales, enfermedades, muerte, asÌ como el deterioro o destrucciÛn de bienes.</p><br/>" +
										  "<ul style='list-style-type:Disc'> " +
											"<li><b>FunciÛn de an·lisis y defensa legal,</b> reclamaciones judiciales y extrajudiciales, hechas al asegurado directamente o por medio de autoridades Administrativas (CONAMED, OIC, Derechos Humanos, Etc.), Juzgados Civiles (Materia Civil), Ministerios P˙blicos y Juzgados de lo Penal, local o federal (Materia Penal), incluye fianzas y cauciones que se requieran en su caso.</li>" +
										 "</ul>" +
								   "</p>" +
								   "</td>" +
								   "</tr>" +

								   "<tr class='ctr'>" +
								   "<td class='ctd' style='width: 20%;'><p><b>Condiciones Especiales:</b></p></td> " +
								   "<td class='ctd' style='width: 60%;'>" +
										"<p>Todos los tÈrminos y condiciones conforme al texto Seguro de responsabilidad civil y de responsabilidad civil profesional para profesiones mÈdicas y sus profesiones auxiliares y tÈcnicas.</p>" +
								   "</td>" +
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
				Text = "<br/><p><b>ìCondiciones Especiales:</b></p><br/>" +
						"<p>-MÈdico Sustituto</p><br/>" +
						"<p>Se especifica que en referencia a la cobertura de sustituciÛn provisional mencionado  en el Condicionado General de la presente pÛliza en el CapÌtulo III, clausula ˙nica de Responsabilidad Civil Profesional, punto 4, inciso b,  se especifica lo siguiente:</p><br/>" +
						"<p>b) El presente seguro se amplÌa a cubrir la responsabilidad cuando al Asegurado, por ausencias temporales encargue a un mÈdico sustituto, de su misma especialidad, la atenciÛn de sus pacientes.</p><br/>" +
						"<p>Dicha condiciÛn aplicar· siempre y cuando la ausencia de dicho asegurado se origine por su participaciÛn en congresos, vacaciones o motivos de fuerza mayor y el paciente tenga el pleno conocimiento y de su consentimiento de ser atendido por el mÈdico sustituto; no ser· materia de cobertura , en caso de que por cualquier circunstancia, se tome la determinaciÛn de realizar alg˙n procedimiento quir˙rgico  que no cuente con los puntos mencionados anteriormente, en estos casos la compaÒÌa aseguradora  no tendr· ninguna responsabilidad, ni har· frente a la  reclamaciÛn derivada de dicha circunstancia.î</p><br/>",
				CSSFont = "p { font-family: Arial; font-size: 10px; text-align: justify; }"
			});
		}

		public static void SlipTradicionalRenov(string numpoliza, bool rc, string nombrecliente, string desc, string esp, string cedprof, string cedesp, string diplo, string sumaaseg, string fecretro, string pol1, string pol2, string pol3)
		{
			docPDF = new DocumentPDF();
			Sections = new List<Section>();

            string Header = "<center><table><tr><td><br/><h2>ESPECIFICACI”N QUE SE ADHIERE Y FORMA PARTE INTEGRANTE DE LA P”LIZA</h2><h2>" + numpoliza + "</h2><br/><br/></td></tr></table></center><br/><br/>";
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
                tblPolAnteriores.Add("<tr class='ctr'><td class='ctd' style='width: 20%;'><p><b>No.de pÛliza 1: </b></p></td><td class='ctd' style='width: 60%;'><p>" + pol1 + "</p></td></tr>");
			if (!String.IsNullOrEmpty(pol2))
				tblPolAnteriores.Add("<tr class='ctr'><td class='ctd' style='width: 20%;'><p><b>No.de pÛliza 2: </b></p></td><td class='ctd' style='width: 60%;'><p>" + pol2 + "</p></td></tr>");
			if (!String.IsNullOrEmpty(pol3))
				tblPolAnteriores.Add("<tr class='ctr'><td class='ctd' style='width: 20%;'><p><b>No.de pÛliza 3: </b></p></td><td class='ctd' style='width: 60%;'><p>" + pol3 + "</p></td></tr>");

			Sections.Add(new Section
			{
				Text = "<table cellpadding='5' class='ctable'>" +
								   "<tr class='ctr'> " +
									   "<td class='ctd' style='width: 20%;'><p><b>Asegurado:</b></p></td> " +
                    "<td class='ctd' style='width: 60%;'><p>" + nombrecliente + "</p></td>" +
								   "</tr>" +
								   "<tr class='ctr'> " +
									   "<td class='ctd' style='width: 20%;'><p><b>EspecificaciÛn:</b></p></td> " +
                    "<td class='ctd' style='width: 60%;'><p>" + desc + "</p></td>" +
								   "</tr>" +
								   "<tr class='ctr'> " +
									   "<td class='ctd' style='width: 20%;'><p><b>Especialidad y/o Subespecialidad:</b></p></td> " +
                    "<td class='ctd' style='width: 60%;'><p>" + esp + "</p></td>" +
								   "</tr>" +
								   "<tr class='ctr'> " +
									   "<td class='ctd' style='width: 20%;'><p><b>CÈdula profesional:</b></p></td> " +
                    "<td class='ctd' style='width: 60%;'><p>" + cedprof + "</p></td>" +
								   "</tr>" +
								   "<tr class='ctr'> " +
									   "<td class='ctd' style='width: 20%;'><p><b>CÈdula de especialidad:</b></p></td> " +
                    "<td class='ctd' style='width: 60%;'><p>" + cedesp + "</p></td>" +
								   "</tr>" +
								   "<tr class='ctr'> " +
									   "<td class='ctd' style='width: 20%;'><p><b>Diplomados u otros estudios:</b></p></td> " +
                    "<td class='ctd' style='width: 60%;'><p>" + diplo + "</p></td>" +
								   "</tr>" +
								   "<tr class='ctr'> " +
									   "<td class='ctd' style='width: 20%;'><p><b>Fecha Convencional</b></p></td> " +
                    "<td class='ctd' style='width: 60%;'><p>" + fecretro + "</p></td>" +
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
									   "<td class='ctd' style='width: 20%;'><p><b>LÌmite M·ximo de Responsabilidad</b></p></td> " +
                    "<td class='ctd' style='width: 60%;'><p>LÌmite M·ximo de Responsabilidad ⁄nico y M·ximo en el agregado anual incluyendo Gastos de Defensa Juridica: " + sumaaseg + "</p></td>" +
								   "</tr>" +
								   "<tr class='ctr'>" +
								   "<td class='ctd' style='width: 20%;'><p><b>Coberturas</b></p></td> " +
								   "<td class='ctd' style='width: 60%;'>" +
								   "<p>" +
										 "<ul style='list-style-type:Disc'> " +
										 "<li><b>Responsabilidad Civil Profesional.</b></li>" +
										 "<li><b>Responsabilidad Civil por el uso de objetos peligrosos (objetiva)</b></li>" +
										 "<li><b>Suministros de medicamentos y materiales de curaciÛn</b></li>" +
										 "<li><b>Responsabilidad Civil por sus inmuebles y Responsabilidad Civil por sus Actividades </b></li>" +
										 "<li><b>Responsabilidad Civil Profesional MÈdica,</b> por la prestaciÛn de sus servicios de la salud, en toda la Rep˙blica Mexicana(Consultorios, torres mÈdicas, Hospitales P˙blicos y Privados, en general todo su ejercicio profesional)</li>" +
										 "<li><b>Responsabilidad Civil inmuebles,</b> derivada del uso y posesiÛn del inmueble de su consultorio.</li>" +
										 "<li><b>Responsabilidad Civil por sus actividades,</b> por ejemplo: Por el uso de piscinas, baÒos, gimnasios, aparatos de rehabilitaciÛn fÌsica, etc.relacionados con las terapias o rehabilitaciones.</li>" +
										 "<li><b>Responsabilidad por el uso de objetos peligrosos</b> (ìobjetivaî). Responsabilidad por el uso de los mecanismos, instrumentos, aparatos o substancias peligrosos por sÌ mismos y que dan lugar a responsabilidad civil. Estos aparatos pueden ser todos los usados para fines del diagnÛstico o de la terapÈutica, en cuanto estÈn reconocidos por la ciencia mÈdica incluyendo el uso de f·rmacos y materiales de curaciÛn.</li>" +
										 "<li><b>AmpliaciÛn a empleados y trabajadores del asegurado.</b> El presente seguro se amplÌa a cubrir la responsabilidad profesional legal de sus empleados y trabajadores en el desempeÒo de sus funciones derivadas de la actividad materia de este seguro, que se indica en la cÈdula de esta pÛliza. Para los efectos de esta pÛliza el m·ximo de empleados, bajo relaciÛn de trabajo, que cubre este seguro es de hasta, en consideraciÛn del ejercicio privado de un profesionista mÈdico:<br/>" +
												"<p>&nbsp;&nbsp;&nbsp;&nbsp;ï 2 secretarias o enfermeras-secretarias</p><br/>" +
												"<p>&nbsp;&nbsp;&nbsp;&nbsp;ï 2 mÈdicos auxiliares</p><br/>" +
												"<p>&nbsp;&nbsp;&nbsp;&nbsp;ï 3 enfermeras</p><br/>" +
										 "</li>" +
										 "<li><b>Medico sustituto.</b>TambiÈn el presente seguro se amplÌa a cubrir la responsabilidad cuando al Asegurado, por ausencias temporales encargue a un mÈdico sustituto, de su misma especialidad, la atenciÛn de sus pacientes.</li>" +
										 "<li>Esta cobertura se amplÌa, en su caso, para <b>herederos legales y cÛnyuges,</b> en caso de fallecimiento del asegurado y que se interponga reclamaciÛn a la masa hereditaria o a la sociedad conyugal respectivamente.</li>" +
										 "<li><b>Plan de asistencia Legal,</b> adicional y sin costo, anexo condiciones particulares.</li>" + cober_rcArrend +
										 "</ul>" +
								   "</p>" +
								   "</td>" +
								   "</tr>" +

								   "<tr class='ctr'> " +
									   "<td class='ctd' style='width: 20%;'><p><b>Riesgo Asegurado:</b></p></td> " +
									   "<td class='ctd' style='width: 60%;'>" +
									   "<p>Seguro de responsabilidad civil y de responsabilidad civil profesional para profesiones mÈdicas y sus profesiones auxiliares y tÈcnicas:</p><br/>" +
									   "<p>GMX Seguros se obliga a pagar la indemnizaciÛn que el Asegurado deba a sus pacientes o a terceros daÒados a consecuencia de uno o m·s hechos realizados sin dolo, ya sea por culpa negligente o imperita (acciones u omisiones), que ocasione en el ejercicio de las profesiones mÈdicas o de las profesiones tÈcnicas o auxiliares de la medicina o por el uso de cosas peligrosas.</p><br/>" +
									   "<p>TambiÈn queda incluida dentro de esta ˙ltima la responsabilidad por el uso de mecanismos, instrumentos, aparatos o substancias peligrosas por sÌ mismos y que dan lugar a responsabilidad civil.</p><br/>" +
									   "<p>Estos aparatos pueden ser todos los usados para fines del diagnÛstico y de la terapÈutica, en cuanto estÈn reconocidos por la ciencia mÈdica, y causen un daÒo previsto en esta pÛliza a terceras personas con motivo de la prestaciÛn de servicios para la salud.</p><br/>" +
									   "<p style='text-align: justify;'>No ser·n materia de cobertura las indemnizaciones adicionales a las que sea condenado el Asegurado por las agravantes con las cuales haya actuado para la realizaciÛn del daÒo, incluso cuando dichas agravantes sean considerados como parte de una indemnizaciÛn identificada bajo el rubro o el concepto de daÒo moral.</p><br/>" +
									   "<p>Para la cobertura de Responsabilidad Civil los daÒos comprenden: lesiones corporales, enfermedades, muerte; asÌ como el deterioro o destrucciÛn de bienes de terceras personas. Los perjuicios que resulten y el daÒo moral sÛlo se cubren cuando sean consecuencia directa e inmediata de los citados daÒos, causados a sus pacientes o a terceros daÒados.</p>" +
									   "</td>" +
								   "</tr>" +

								   "<tr class='ctr'> " +
										"<td class='ctd' style='width: 20%;'><p><b>Base de IndemnizaciÛn integrantes nuevos:</b></p></td> " +
										"<td class='ctd' style='width: 60%;'>" +
											"<p>De acuerdo con lo previsto en el art. 145 bis de la Ley sobre el Contrato de Seguro, se otorga fecha convencional desde el inicio de vigencia de la primera pÛliza contratada con GMX Seguros, siempre que las renovaciones hayan sido una cadena ininterrumpida de seguros con GMX Seguros, ello sobre hechos no conocidos ni reclamados previamente al Asegurado o a GMX Seguros y siempre que la reclamaciÛn se formule por primera vez y por escrito al Asegurado o a GMX Seguros, durante la vigencia actual de la pÛliza. Se aclara que en caso de reclamaciÛn, aplicar·n los lÌmites y las condiciones que prevalecen en la pÛliza que corresponde al aÒo de la reclamaciÛn, es decir, las condiciones de la pÛliza vigente en el momento de la presentaciÛn de la reclamaciÛn al asegurado o a GMX Seguros, lo que ocurra primero, sujet·ndose al marco de las cl·usulas de la citada pÛliza.</p><br/>" +
											"<p>Se aclara adem·s que en este caso las disposiciones del Pre·mbulo y de la letra b) de la Cl·usula 1™ del CapÌtulo I de la pÛliza (condiciones generales) se modifican para quedar como sigue:</p><br/>" +
											"<p>ìPre·mbulo: El presente contrato de seguro se celebra conforme a lo dispuesto en el inciso b) del Art. 145 bis de la Ley sobre el Contrato de Seguros, para cubrir la indemnizaciÛn que el Asegurado deba a un tercero por hechos ocurridos desde la fecha convencional, siempre que la reclamaciÛn se formule por primera vez y por escrito al Asegurado o a GMX Seguros, en el curso de la vigencia de la pÛliza.î</p><br/>" +
											"<p>CapÌtulo I, Cl·usula 1™:</p>" +
											"<p>ìb) Base de indemnizaciÛn.</p>" +
											"<p>El presente seguro cubre la indemnizaciÛn que el Asegurado deba a un tercero, conforme a las condiciones pactadas en el presente contrato, por hechos ocurridos desde la fecha convencional, siempre que la reclamaciÛn se formule por primera vez y por escrito al Asegurado o a GMX Seguros, en el curso de la vigencia de esta pÛliza.î</p><br/>" +
											"<p><b>FECHA CONVENCIONAL:</b></p><br/>" +
											"<p>De acuerdo con lo previsto en el art. 145 bis de la Ley sobre el Contrato de Seguro, se otorga fecha convencional desde el inicio de vigencia de la primera pÛliza contratada con GMX Seguros, siempre que las renovaciones hayan sido una cadena ininterrumpida de seguros con GMX Seguros, ello sobre hechos no conocidos ni reclamados previamente al Asegurado o a GMX Seguros y siempre que la reclamaciÛn se formule por primera vez y por escrito al Asegurado o a GMX Seguros, durante la vigencia actual de la pÛliza.</p><br/>" +
											"<p>Se aclara que en caso de reclamaciÛn, aplicar·n los lÌmites y las condiciones que prevalecen en la pÛliza que corresponde al aÒo del hecho generador que propicia la reclamaciÛn, es decir, las condiciones de la pÛliza vigente en el momento del hecho generador del daÒo, sujet·ndose al marco de las cl·usulas de la citada pÛliza.</p><br/>" +
											"<p><b>NOTA IMPORTANTE: Para que la fecha convencional sea reconocida ser· necesario que el asegurado acredite la continuidad de pÛlizas pagadas con GMX Seguros sin periodos descubiertos.</b></p><br/>" +
										"</td>" +
								   "</tr>" +

								   "<tr class='ctr'>" +
								   "<td class='ctd' style='width: 20%;'><p><b>Exclusiones:</b></p></td> " +
								   "<td class='ctd' style='width: 60%;'>" +
								   "<p>" +
										 "<ul style='list-style-type:Disc'> " +
											"<li><b>Acciones dolosas de parte del asegurado</b></li>" +
											"<li><b>GarantÌa de calidad del servicio.</b></li>" +
											"<li><b>Incumplimiento de contratos.</b></li>" +
											"<li><b>Obligaciones derivadas de un contrato.</b></li>" +
											"<li><b>R.C. Productos, incluyendo dentro de Èstos los farmacÈuticos y transgÈnicos.</b></li>" +
											"<li><b>R.C. Patronal.</b></li>" +
											"<li><b>R.C. Contractual.</b></li>" +
											"<li><b>Dem·s que seÒalan las condiciones generales</b></li>" +
										 "</ul>" +
								   "</p>" +
								   "</td>" +
								   "</tr>" +

								   "<tr class='ctr'>" +
								   "<td class='ctd' style='width: 20%;'><p><b>Deducibles:</b></p></td> " +
								   "<td class='ctd' style='width: 60%;'>" +
								   "<p>" +
										 "<ul style='list-style-type:Disc'> " +
											"<li>Para daÒos a terceros en sus personas: Sin deducible.</li>" +
											"<li>Para daÒos a terceros en sus propiedades: Sin deducible.</li>" +
										 "</ul>" +
								   "</p>" +
								   "</td>" +
								   "</tr>" +

								   "<tr class='ctr'>" +
								   "<td class='ctd' style='width: 20%;'><p><b>Funciones del seguro:</b></p></td> " +
								   "<td class='ctd' style='width: 60%;'>" +
								   "<p>" +
										 "<ul style='list-style-type:Disc'> " +
											"<li><b>FunciÛn indemnizatoria,</b> que deba el asegurado a su paciente derivado de una negligencia, impericia o el uso de sustancias, aparatos u objetos que por su propia naturaleza causen un daÒo al mismo, asÌ como el los perjuicios y daÒo moral, siempre y cuando sea a consecuencia directa e inmediata del daÒo causado.</li>" +
										 "</ul>" +
										 "<br/><br/><p>Los daÒos comprenden lesiones corporales, enfermedades, muerte, asÌ como el deterioro o destrucciÛn de bienes.</p><br/>" +
										  "<ul style='list-style-type:Disc'> " +
											"<li><b>FunciÛn de an·lisis y defensa legal,</b> reclamaciones judiciales y extrajudiciales, hechas al asegurado directamente o por medio de autoridades Administrativas (CONAMED, OIC, Derechos Humanos, Etc.), Juzgados Civiles (Materia Civil), Ministerios P˙blicos y Juzgados de lo Penal, local o federal (Materia Penal), incluye fianzas y cauciones que se requieran en su caso.</li>" +
										 "</ul>" +
								   "</p>" +
								   "</td>" +
								   "</tr>" +

								   "<tr class='ctr'>" +
								   "<td class='ctd' style='width: 20%;'><p><b>Condiciones Especiales:</b></p></td> " +
								   "<td class='ctd' style='width: 60%;'>" +
										"<p>Todos los tÈrminos y condiciones conforme al texto Seguro de responsabilidad civil y de responsabilidad civil profesional para profesiones mÈdicas y sus profesiones auxiliares y tÈcnicas.</p>" +
								   "</td>" +
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
				Text = "<br/><p><b>ìCondiciones Especiales:</b></p><br/>" +
			"<p>-MÈdico Sustituto</p><br/>" +
			"<p>Se especifica que en referencia a la cobertura de sustituciÛn provisional mencionado  en el Condicionado General de la presente pÛliza en el CapÌtulo III, clausula ˙nica de Responsabilidad Civil Profesional, punto 4, inciso b,  se especifica lo siguiente:</p><br/>" +
			"<p>b) El presente seguro se amplÌa a cubrir la responsabilidad cuando al Asegurado, por ausencias temporales encargue a un mÈdico sustituto, de su misma especialidad, la atenciÛn de sus pacientes.</p><br/>" +
			"<p>Dicha condiciÛn aplicar· siempre y cuando la ausencia de dicho asegurado se origine por su participaciÛn en congresos, vacaciones o motivos de fuerza mayor y el paciente tenga el pleno conocimiento y de su consentimiento de ser atendido por el mÈdico sustituto; no ser· materia de cobertura , en caso de que por cualquier circunstancia, se tome la determinaciÛn de realizar alg˙n procedimiento quir˙rgico  que no cuente con los puntos mencionados anteriormente, en estos casos la compaÒÌa aseguradora  no tendr· ninguna responsabilidad, ni har· frente a la  reclamaciÛn derivada de dicha circunstancia.î</p><br/>",
				CSSFont = "p { font-family: Arial; font-size: 10px; text-align: justify; }"
			});
		}
        #endregion Tradicional


        #region Angeles
        #endregion Angeles


        #region Cotizacion
        public static void GetSlipCotizacionAng(VMCotizar vm)
        {
            docPDF = new DocumentPDF();
            Sections = new List<Section>();

            docPDF.IsCotizacion = true;
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
                Text = "<table style='width: 100%;'><tr><td style='text-align: right'><p>Fecha de expediciÛn: " + Today + "</p></td></tr></table><br/>"
               ,
                CSSFont = "p{ font-size: 12; font-family : Arial; text-align: right;}"
            });
            #endregion
            #region BENEFICIO Dirigido

            Sections.Add(new Section
            {
                Text = "<table style='width: 100%;'>"
                        + "<tr><td style='width: 500px;'></td><td style='text-align: left'><p><b>BENEFICIO Dirigido exclusivamente para MÈdicos pertenecientes a Grupo ¡ngeles (Nacional)</b></p></td></tr>"
                        + "<tr><td style='width: 500px;'></td><td style='text-align: left'><p><b>Ref.:Seguro GMX de RC, </b><i>Seguro de responsabilidad civil y responsabilidad profesional para las profesiones MÈdicas</i></p></td></tr>"
                        + "</table><br/>"
               ,
                CSSFont = "p{ font-size: 12; font-family : Arial; text-align: left;}"
            });
            #endregion
            #region BODY
            Sections.Add(new Section
            {
                Text = "<h1 style='text-align:left;'><p><b>Estimado MÈdico que pertenece a Grupo ¡ngeles:</b></p></h1><br/>" +
                        "<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;GMX Seguros es una aseguradora 100% mexicana, comprometida con los profesionales al cuidado de la salud, lÌder en MÈxico en seguros de responsabilidad civil y particularmente en responsabilidad civil profesional mÈdica con m·s de 250, 000 MÈdicos Asegurados.</p><br/>" +
                        "<p>A continuaciÛn nos permitimos exponer las caracterÌsticas y ventajas de nuestro producto:</p><br/>"
                        ,
                CSSFont = "h1{ font-size: 18; font-family : Arial; text-align: left;} p{ font-size: 12; font-family : Arial; text-align: justify;}"

            });
            Sections.Add(new Section
            {
                Text = "<ol class='a'>" +
                "<li value='1'><pre>Indemnizar al paciente afectado o alg˙n tercero para cubrir los montos que deba el asegurado a un paciente derivado de un acto negligente o imperito o por el uso de sustancias, aparatos u objetos que por su propia naturaleza causen un daÒo, asÌ como el pago del daÒo moral y el perjuicio.</pre></li>" +
                "<li>Defensa jurÌdica, reclamaciones judiciales y extrajudiciales hechas al Asegurado directamente o por medio de:" +
                "<ul style='font-family: Arial; font-size: 12px; list-style-type: disc; text-align: justify;'>" +
                "<li style='list-style-type: circle'>Autoridades Administrativas (<b>CONAMED</b>, Derechos Humanos, ”rganos Internos de control, FunciÛn P˙blica, por mencionar algunos) </li>" +
                "<li style='list-style-type: circle'>Materia Civil (<b>Juzgados Civiles</b>)</li>" +
                "<li style='list-style-type: circle'>Materia Penal (<b>Ministerios P˙blicos y Juzgados Penales</b>). Todos los anteriores tanto federales como del fuero com˙n,</li>" +
                "</ul></li></ol>"
                ,
                CSSFont = "ol.a {font-family: Arial; font-size: 12; list-style-type: decimal; text-align: justify;} p{ font-size: 12; font-family : Arial; text-align: justify;}"
            });
            Sections.Add(new Section
            {
                Text = "<br/><p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;En esta cobertura se le asigna abogado especialista, de acuerdo al caso en concreto y al lugar donde se le estÈ reclamando, y el muy importante pago, de primas de <b>fianzas y las cauciones en efectivo.</b></p><br/>"
                        + "<p><b>Beneficios:</b></p>"
                        + "<ul style='font-family: Arial; font-size: 12px; list-style-type: disc; text-align: justify;'>"
                        + "<li style='list-style-type: circle'>Los Asegurados de GMX Seguros, tendr·n cobertura retroactiva desde la primera pÛliza que se contratÛ con nosotros (fecha convencional), sin perder sus derechos.</li>"
                        + "<li style='list-style-type: circle'>Nuestra cobertura b·sica es: <b>SIN DEDUCIBLE</b></li>"
                        + "<li style='list-style-type: circle'>La atenciÛn de siniestros, de alta especializaciÛn con una trayectoria ˙nica en el mercado mexicano, por la<b> experiencia de m·s 250 mil mÈdicos asegurados.</b></li>"
                        + "</ul>"
                ,
                CSSFont = "p{ font-size: 12; font-family : Arial; text-align: justify;}"

            });
            Sections.Add(new Section { onlyNewPage = true });
            Sections.Add(new Section
            {
                Text = "<p><b>Cobertura*:</b></p><ol class='a'>" +
                "<li>Responsabilidad Civil Profesional MÈdica por el ejercicio de su profesiÛn, por alguna reclamaciÛn o demanda de parte de sus pacientes derivado de su ejercicio profesional.</li>" +
                "<li>Responsabilidad Civil por sus inmuebles y por sus actividades, por ejemplo:" +
                "<ul style='font-family: Arial; font-size: 12px; list-style-type: square; text-align: justify;'>" +
                "<li>DaÒos causados a los pacientes o a terceros derivados del uso de instalaciones higiÈnicas, y relacionadas con las terapias o rehabilitaciones.</li>" +
                "</ul></li>"
                + "<li>Responsabilidad Civil como arrendatario y condÛmino, en caso de que rente su consultorio.</li>"
                + "<li>Responsabilidad Civil Privada y Familiar, hasta un SublÌmite de $100,000.00 MN </li>"
                + "</ol>"
                ,
                CSSFont = "ol.a {font-family: Arial; font-size: 12; list-style-type: disc; text-align: justify;}  p{ font-size: 12; font-family : Arial; text-align: justify;}"
            });
            Sections.Add(new Section
            {
                Text = "<br/><table cellpadding='5' class='ctable'><tr class='ctr'><td class='ctd'><p><b>LÌmite de responsabilidad:</b></p></td><td class='ctd'><p><b>Prima NETA:</b></p></td></tr> " + pNeta + "  </table>"
                        + "<p><b>Nota: A las primas anteriores se debe de agregar los derechos de pÛliza ($350.00 M.N.) e IVA correspondiente.</b></p><br/>"
                        + "<p><b>Se especifica que la entrada en vigor del presente comunicado es a partir del 04 de Julio de 2016.</b></p><br/>"
                        + "<p><i>De acuerdo a los tÈrminos, condiciones particulares y generales del texto de GMX Seguro, definidos para este proyecto.</i></p>"
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
        #endregion Cotizacion
    }
}
