using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using SCME.Types.Profiles;
using SCME.CustomControls;
using SCME.Types;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using OpenXml;
using static SCME.dbViewer.ForParameters.ReportData;

namespace SCME.dbViewer.ForParameters
{
    public class ReportRecord
    {
        public ReportRecord(ReportData owner, DynamicObj row)
        {
            this.Owner = owner;
            this.Row = row;
        }

        public ReportData Owner { get; set; }
        public DynamicObj Row { get; set; }

        /*
        public string ProfileName
        {
            get
            {
                //профиль хранится в скрытом поле под именем тела профиля
                string name = Routines.NameOfHiddenColumn("PROFILEBODY");

                if (this.Row.GetMember(name, out object value))
                {
                    return value.ToString();
                }
                else
                    return null;
            }
        }
        */

        public string Prof_ID
        {
            get
            {
                return (this.Row.GetMember("PROF_ID", out object value)) ? value?.ToString() : null;
            }
        }

        public string AssemblyProtocolID
        {
            get
            {
                return (this.Row.GetMember(Common.Constants.AssemblyProtocolID, out object value)) ? value?.ToString() : null;
            }
        }

        public string AssemblyProtocolDescr
        {
            get
            {
                return (this.Row.GetMember(Common.Constants.AssemblyProtocolDescr, out object value)) ? value?.ToString() : null;
            }
        }

        public string ProfileBody
        {
            get
            {
                return (this.Row.GetMember("PROFILEBODY", out object value)) ? value?.ToString() : null;
            }
        }

        public string GroupName
        {
            get
            {
                return (this.Row.GetMember(Common.Constants.GroupName, out object value)) ? value?.ToString() : null;
            }
        }

        public string Code
        {
            get
            {
                return (this.Row.GetMember(Common.Constants.Code, out object value)) ? value?.ToString() : null;
            }
        }

        public string CodeSimple
        {
            get
            {
                string result = this.Code;
                int posDelimeter = result.IndexOf("/");

                if (posDelimeter != -1)
                    result = result.Remove(posDelimeter);

                return result;
            }
        }

        public string JobFromCode
        {
            get
            {
                string result = this.Code;
                int posDelimeter = result.IndexOf("/");

                if (posDelimeter != -1)
                    result = result.Remove(0, posDelimeter + 1);

                return result;
            }
        }

        public string Item
        {
            get
            {
                return (this.Row.GetMember(Common.Constants.Item, out object value)) ? value?.ToString() : null;
            }
        }

        public string MmeCode
        {
            get
            {
                return (this.Row.GetMember(Common.Constants.MmeCode, out object value)) ? value?.ToString() : null;
            }
        }

        /*
        public string PairMmeCode
        {
            get
            {
                int columnIndex = this.Row.Table.Columns.IndexOf(Routines.NameOfHiddenColumn(Constants.MmeCode));

                return this.Row[columnIndex].ToString();

                return null;
            }
        }
        */

        public string Ts
        {
            get
            {
                return (this.Row.GetMember(Common.Constants.Ts, out object value)) ? value?.ToString() : null;
            }
        }

        /*
        public string PairTs
        {
            get
            {
                int columnIndex = this.Row.Table.Columns.IndexOf(Routines.NameOfHiddenColumn(Constants.Ts));

                return DateTime.Parse(this.Row[columnIndex].ToString());

                return null;
            }
        }
        */

        public string DeviceClass
        {
            get
            {
                return (this.Row.GetMember(Common.Constants.DeviceClass, out object value)) ? value?.ToString() : null;
            }
        }

        public string Status
        {
            get
            {
                return (this.Row.GetMember(Common.Constants.Status, out object value)) ? value?.ToString() : null;
            }
        }

        public string CodeOfNonMatch
        {
            get
            {
                return (this.Row.GetMember(Common.Constants.CodeOfNonMatch, out object value)) ? value?.ToString() : null;
            }
        }

        public string AssemblyJob
        {
            get
            {
                return (this.Row.GetMember(Constants.AssemblyJob, out object value)) ? value?.ToString() : null;
            }
        }

        public string PackageType
        {
            get
            {
                return (this.Row.GetMember(Constants.PackageType, out object value)) ? value?.ToString() : null;
            }
        }

        public string Omnity
        {
            get
            {
                return (this.Row.GetMember(Constants.Omnity, out object value)) ? value?.ToString() : null;
            }
        }

        public string Device
        {
            get
            {
                return (this.Row.GetMember(Constants.Device, out object value)) ? value?.ToString() : null;
            }
        }

        public string DeviceTypeRu
        {
            get
            {
                return (this.Row.GetMember(Constants.DeviceTypeRu, out object value)) ? value?.ToString() : null;
            }
        }

        public string Tq
        {
            get
            {
                if (this.Row.GetMember(Constants.Tq, out object value))
                {
                    //в случае выбора пользователем не определённого значения "-" будем возвращать null
                    return (value == null) ? null : Routines.ValueAsDouble(value.ToString(), out double d) ? value.ToString() : null;
                }
                else
                    return null;
            }
        }

        public string Trr
        {
            get
            {
                return (this.Row.GetMember(Constants.Trr, out object value)) ? value?.ToString() : null;
            }
        }

        public string Qrr
        {
            get
            {
                return (this.Row.GetMember(Constants.Qrr, out object value)) ? value?.ToString() : null;
            }
        }

        public string DUdt
        {
            get
            {
                return (this.Row.GetMember(Constants.dUdt, out object value)) ? value?.ToString() : null;
            }
        }

        public string DUdtUnitMeasure
        {
            get
            {
                return (this.Row.GetMember(Routines.NameOfUnitMeasure(Constants.dUdt), out object value)) ? value.ToString() : null;
            }

            set
            {
                this.Row.SetMember(Routines.NameOfUnitMeasure(Constants.dUdt), value);
            }
        }

        public double IgtMin
        {
            get
            {
                if (this.Row.GetMember(string.Concat(Constants.Igt, Constants.Min), out object value))
                {
                    return (Common.Routines.TryStringToDouble(value?.ToString(), out double result)) ? result : 0;
                }
                else
                    return 0;
            }

            set
            {
                this.Row.SetMember(string.Concat(Constants.Igt, Constants.Min), value);
            }
        }

        public double IgtMax
        {
            get
            {
                if (this.Row.GetMember(string.Concat(Constants.Igt, Constants.Max), out object value))
                {
                    return (Common.Routines.TryStringToDouble(value?.ToString(), out double result)) ? result : 0;
                }
                else
                    return 0;
            }

            set
            {
                this.Row.SetMember(string.Concat(Constants.Igt, Constants.Max), value);
            }
        }

        public string IgtUnitMeasure
        {
            get
            {
                return (this.Row.GetMember(Routines.NameOfUnitMeasure(Constants.Igt), out object value)) ? value.ToString() : null;
            }

            set
            {
                this.Row.SetMember(Routines.NameOfUnitMeasure(Constants.Igt), value);
            }
        }

        public double UgtMin
        {
            get
            {
                if (this.Row.GetMember(string.Concat(Constants.Ugt, Constants.Min), out object value))
                {
                    return (Common.Routines.TryStringToDouble(value?.ToString(), out double result)) ? result : 0;
                }
                else
                    return 0;
            }

            set
            {
                this.Row.SetMember(string.Concat(Constants.Ugt, Constants.Min), value);
            }
        }

        public double UgtMax
        {
            get
            {
                if (this.Row.GetMember(string.Concat(Constants.Ugt, Constants.Max), out object value))
                {
                    return (Common.Routines.TryStringToDouble(value?.ToString(), out double result)) ? result : 0;
                }
                else
                    return 0;
            }

            set
            {
                this.Row.SetMember(string.Concat(Constants.Ugt, Constants.Max), value);
            }
        }

        public string UgtUnitMeasure
        {
            get
            {
                return (this.Row.GetMember(Routines.NameOfUnitMeasure(Constants.Ugt), out object value)) ? value.ToString() : null;
            }

            set
            {
                this.Row.SetMember(Routines.NameOfUnitMeasure(Constants.Ugt), value);
            }
        }

        public string Tgt
        {
            get
            {
                return (this.Row.GetMember(Constants.Tgt, out object value)) ? value?.ToString() : null;
            }
        }

        public string AssemblyReportRecordCount
        {
            get
            {
                return (this.Row.GetMember(Constants.AssemblyReportRecordCount, out object value)) ? value?.ToString() : null;
            }
        }

        public string QtyReleasedByGroupName
        {
            get
            {
                return (this.Row.GetMember(Constants.QtyReleasedByGroupName, out object value)) ? value?.ToString() : null;
            }
        }

        public string ColumnsSignature
        {
            get
            {
                //формируем список столбцов, в которых содержатся данные
                //начинаем с наименования ПЗ для возможности выполнения желаемой сортировки записей
                string result = this.GroupName; //string.Concat(this.GroupName, this.ProfileBody)

                //наименования столбцов реквизитов - идентификаторов (любой столбец не являющийся conditions/parameters) игнорируем, т.к. они есть в любой записи
                int conditionsInDataSourceFirstIndex = this.ConditionsFirstIndex();
                int parametersInDataSourceFirstIndex = this.ParametersFirstIndex();

                if (conditionsInDataSourceFirstIndex != -1)
                {
                    List<string> memberNames = this.Row.GetDynamicMemberNames().ToList();

                    //нам важно, чтобы при сортировке по возвращаемому result первая запись всегда имела первым тепловым режимом режим RT
                    string temperatureCondition = memberNames[conditionsInDataSourceFirstIndex].Substring(0, 2);
                    result = string.Concat(result, temperatureCondition);

                    for (int i = conditionsInDataSourceFirstIndex; i < memberNames.Count(); i++)
                    {
                        string currentColumnName = memberNames[i];

                        //скрытые столбцы в отчёт не выводятся - игнорируем их при построении результата данной реализации
                        if ((!string.IsNullOrEmpty(currentColumnName)) && (!currentColumnName.Contains(Constants.HiddenMarker)))
                        {
                            result = string.Concat(result, currentColumnName);

                            if (i >= parametersInDataSourceFirstIndex)
                            {
                                string nrmDescr = null;
                                nrmDescr = this.NrmByColumnName(currentColumnName);

                                if (!string.IsNullOrEmpty(nrmDescr))
                                    result = string.Concat(result, nrmDescr);
                            }
                        }
                    }
                }

                return result;
            }
        }

        private string TemperatureDelimeter
        {
            get
            {
                return "\n";
            }
        }

        public string TemperatureByColumnName(string columnName)
        {
            //считывает значение температуры при которой применялось condition с именем columnName, либо выполнялось измерение параметра с именем columnName
            string result = null;

            string nameOfHiddenColumn = Routines.NameOfHiddenColumn(columnName);

            if (this.Row.GetMember(nameOfHiddenColumn, out object value))
            {
                //нам нужна только температура
                string[] stringSeparators = new string[] { Constants.StringDelimeter };
                result = value.ToString().Split(stringSeparators, StringSplitOptions.None)[0];
            }

            return result;
        }

        public string NrmByColumnName(string columnName)
        {
            //считывает описание норм для значения измеряемого параметра с именем columnName
            string nameOfNrmMin = Routines.NameOfNrmMinParametersColumn(columnName);

            if (!this.Row.GetMember(nameOfNrmMin, out object valueNrmMin))
                valueNrmMin = null;

            string nameOfNrmMax = Routines.NameOfNrmMaxParametersColumn(columnName);

            if (!this.Row.GetMember(nameOfNrmMax, out object valueNrmMax))
                valueNrmMax = null;

            string result = Routines.NrmDescr(valueNrmMin, valueNrmMax);

            /*
            string nameOfNrmMin = Routines.NameOfNrmMinParametersColumn(columnName);

            if (this.Row.GetMember(nameOfNrmMin, out object valueNrmMin))
            {
                string nrmMin = valueNrmMin.ToString();
                result = (nrmMin == string.Empty) ? string.Empty : string.Concat(nrmMin, "<X");
            }

            string nameOfNrmMax = Routines.NameOfNrmMaxParametersColumn(columnName);

            if (this.Row.GetMember(nameOfNrmMax, out object valueNrmMax))
            {
                string nrmMax = valueNrmMax.ToString();
                nrmMax = (nrmMax == string.Empty) ? string.Empty : string.Concat("≤", nrmMax);

                if (string.IsNullOrEmpty(result) && (nrmMax != string.Empty))
                    result = "X";

                result = string.Concat(result, nrmMax);
            }
            */

            return result;
        }

        private bool PairExists()
        {
            //отвечает на вопрос о наличии группированных данных в this.Row
            bool result = false;

            if (this.Row.GetMember(Common.Constants.DevID, out object valueDevID))
                result = valueDevID.ToString().Contains(Constants.DelimeterForStringConcatenate);

            return result;
        }

        public string RCColumnNumToA1ColumnNum(int columnNum)
        {
            //преобразование номера столбца ColumnNum из цифровой идентификации в строковую идентификацию
            int A1 = Convert.ToByte('A') - 1;  //номер "A" минус 1 (65 - 1 = 64)

            int AZ = Convert.ToByte('Z') - A1; //кол-во букв в англ. алфавите (90 - 64 = 26)
            int t, m;

            //номер колонки
            t = (int)(columnNum / AZ); //целая часть
            m = (columnNum % AZ);      //остаток

            if (m == 0)
                t--;

            char result = '\0';

            switch (t > 0)
            {
                case (true):
                    result = Convert.ToChar(A1 + t);
                    break;
            }

            switch (m)
            {
                case (0):
                    t = AZ;
                    break;

                default:
                    t = m;
                    break;
            }

            string Result = "";
            if (result != '\0')
                Result = Convert.ToString(result);

            return Result + Convert.ToString(Convert.ToChar(A1 + t));
        }

        public string xlRCtoA1(int rowNum, int columnNum)
        {
            //преобразование цифровой идентификации ячейки (по номеру столбца, номеру строки) в строковую идентификацию
            return "$" + RCColumnNumToA1ColumnNum(columnNum) + rowNum.ToString();
        }

        public void TopHeaderToExcel(SpreadsheetDocument spreadsheetDocument, ref uint rowNum)
        {
            //выводим верхнюю часть заголовка
            if (spreadsheetDocument != null)
            {
                uint column = 1;
                uint styleIndex = OpenXmlRoutines.CreateStyle(spreadsheetDocument, "Arial Narrow", 11, false, PatternValues.None, "0", true, HorizontalAlignmentValues.Center, VerticalAlignmentValues.Bottom, typeof(string));
                OpenXmlRoutines.MergeCells(spreadsheetDocument, rowNum, column, rowNum, column + 1, "ПРОТОКОЛ ИСПЫТАНИЙ", styleIndex);
                column += 2;

                //выводим тело профиля
                string value = string.Concat(this.DeviceTypeRu, " ", this.ProfileBody);
                OpenXmlRoutines.MergeCells(spreadsheetDocument, rowNum, column, rowNum, column + 3, value, styleIndex);
                rowNum++;
                column = 1;

                OpenXmlRoutines.MergeCells(spreadsheetDocument, rowNum, column, rowNum, column + 1, "Код ТМЦ", styleIndex);
                column += 2;

                OpenXmlRoutines.MergeCells(spreadsheetDocument, rowNum, column, rowNum, column + 3, this.Item, styleIndex);
                rowNum++;
                column = 1;

                OpenXmlRoutines.MergeCells(spreadsheetDocument, rowNum, column, rowNum, column + 1, "№ ПЗ", styleIndex);
                column += 2;

                OpenXmlRoutines.MergeCells(spreadsheetDocument, rowNum, column, rowNum, column + 3, this.GroupName, styleIndex);
                rowNum++;
            }
        }

        public int ConditionsFirstIndex()
        {
            return Routines.ConditionsFirstIndex(this.Row);
        }

        public int ParametersFirstIndex()
        {
            return Routines.ParametersFirstIndex(this.Row);
        }

        private string ColumnName(string columnName, Common.Routines.XMLValues subject, TemperatureCondition temperatureCondition)
        {
            string result = null;

            if (!string.IsNullOrEmpty(columnName))
            {
                //извлекаем из columnName имя и индекс
                string pcName = Routines.PCName(columnName, out int? index);

                if (!string.IsNullOrEmpty(pcName))
                {
                    switch (subject)
                    {
                        case Common.Routines.XMLValues.Conditions:
                            result = string.Concat(Dictionaries.ConditionName(temperatureCondition, pcName), index?.ToString());
                            break;

                        case Common.Routines.XMLValues.Parameters:
                            result = string.Concat(Dictionaries.ParameterName(pcName), index?.ToString()); // (test == DbRoutines.cManually.ToUpper()) ? columnName : Dictionaries.ParameterName(columnName);
                            break;

                        case Common.Routines.XMLValues.ManuallyParameters:
                            result = columnName;
                            break;

                        default:
                            result = null;
                            break;
                    }
                }
            }

            return result;
        }

        private bool IsDeviceTypeRuMStarting(string deviceTypeRu)
        {
            if (string.IsNullOrEmpty(deviceTypeRu))
            {
                return false;
            }
            else
            {
                string deviceTypeStarting = deviceTypeRu.Substring(0, 1).ToLowerInvariant();

                return (deviceTypeStarting == "м");
            }
        }

        public void HeaderCPToExcel(SpreadsheetDocument spreadsheetDocument, ref uint rowNum, ref uint column)
        {
            //вывод наименований столбцов условий и параметров изделия
            if (spreadsheetDocument != null)
            {
                uint columnsCount = 0;

                int conditionsInDataSourceFirstIndex = this.ConditionsFirstIndex();
                int parametersInDataSourceFirstIndex = this.ParametersFirstIndex();

                //идём по столбцам условий и параметров 1-го температурного режима
                if ((conditionsInDataSourceFirstIndex != -1) || (parametersInDataSourceFirstIndex != -1))
                {
                    int startIndex = (conditionsInDataSourceFirstIndex == -1) ? parametersInDataSourceFirstIndex : conditionsInDataSourceFirstIndex;

                    List<string> memberNames = this.Row.GetDynamicMemberNames().ToList();

                    //пользователь хочет видеть в отчёте только те параметры, которые перечислены в Constants.OrderedColumnNamesInReport + любые созданные вручную параметры                   
                    //в итоге в names получаем все возможные параметры, которые в принципе может содержать отчёт и при этом они имеют желаемый порядок
                    IEnumerable<string> manuallyParameters = memberNames.Where(n => (memberNames.IndexOf(n) >= startIndex) && n.Contains("manuallyparameters") && !n.Contains(Constants.HiddenMarker));

                    //каждый элемент списка manuallyParameters в своём имени содержит не нужные нам сейчас данные - извлекаем только имя параметра
                    //имя параметра начинается за разделителем SCME.Common.Constants.FromXMLNameSeparator и до конца строки
                    //дописываем в конец names все имеющиеся в обрабатываемом this.Row вручную созданные параметры, при этом порядок их следования пользователю не важен
                    //формируем в names список всех имён параметров, которые хочет видеть пользователь: Constants.OrderedColumnNamesInReport + созданные вручную параметры
                    List<string> names = Constants.OrderedColumnNamesInReport.ToList();
                    names.AddRange(manuallyParameters.Select(n => n.Substring(n.IndexOf(SCME.Common.Constants.FromXMLNameSeparator) + SCME.Common.Constants.FromXMLNameSeparator.Length)));

                    uint styleIndex = OpenXmlRoutines.CreateStyle(spreadsheetDocument, "Arial Narrow", 11, false, PatternValues.None, "0", true, HorizontalAlignmentValues.Center, VerticalAlignmentValues.Bottom, typeof(string));
                    uint styleBoldIndex = OpenXmlRoutines.CreateStyle(spreadsheetDocument, "Arial Narrow", 11, true, PatternValues.None, "0", true, HorizontalAlignmentValues.Center, VerticalAlignmentValues.Bottom, typeof(string));

                    foreach (string name in names)
                    {
                        //вычисляем список мест хранения в memberNames: по вычисленному name может быть найдено множество мест хранения с именем name
                        //каждое из этих мест имеет свой температурный режим
                        IEnumerable<string> listOfColumnNames = memberNames.Where(n => (memberNames.IndexOf(n) >= startIndex) && n.Contains(string.Concat(SCME.Common.Constants.FromXMLNameSeparator, name)) && !n.Contains(Constants.HiddenMarker));

                        if (listOfColumnNames != null)
                        {
                            foreach (string columnName in listOfColumnNames)
                            {
                                string trueName = Routines.ParseColumnName(columnName, out string test, out string temperatureCondition);

                                if ((trueName != null) && Enum.TryParse(temperatureCondition, out TemperatureCondition tc))
                                {
                                    int columnIndex = memberNames.IndexOf(columnName);
                                    Common.Routines.XMLValues subject = (columnIndex < parametersInDataSourceFirstIndex) ? Common.Routines.XMLValues.Conditions : (test == DbRoutines.cManually.ToUpper()) ? Common.Routines.XMLValues.ManuallyParameters : Common.Routines.XMLValues.Parameters;

                                    //выводим температуру при которой выполнено измерение данного параметра
                                    uint columnIndexInExcel = column + columnsCount;

                                    string value = string.Concat(temperatureCondition, " ", this.TemperatureByColumnName(columnName));
                                    Cell cell = OpenXmlRoutines.SetCellValue(spreadsheetDocument, rowNum, columnIndexInExcel, value);
                                    cell.StyleIndex = styleIndex;

                                    //выводим имя условия/параметра
                                    value = this.ColumnName(trueName, subject, tc);
                                    cell = OpenXmlRoutines.SetCellValue(spreadsheetDocument, rowNum + 1, columnIndexInExcel, value);
                                    cell.StyleIndex = styleBoldIndex;

                                    //выводим единицу измерения
                                    string nameOfUnitMeasure = Routines.NameOfUnitMeasure(columnName);

                                    if (this.Row.GetMember(nameOfUnitMeasure, out object unitMeasure))
                                    {
                                        cell = OpenXmlRoutines.SetCellValue(spreadsheetDocument, rowNum + 2, columnIndexInExcel, unitMeasure.ToString());
                                        cell.StyleIndex = styleIndex;
                                    }

                                    //выводим норму
                                    string nrmDescr = this.NrmByColumnName(columnName);

                                    if (nrmDescr != null)
                                    {
                                        cell = OpenXmlRoutines.SetCellValue(spreadsheetDocument, rowNum + 3, columnIndexInExcel, nrmDescr);
                                        cell.StyleIndex = styleBoldIndex;
                                    }

                                    //считаем сколько мы вывели новых столбцов
                                    columnsCount++;
                                }
                            }
                        }
                    }
                }

                rowNum += 3;
                column += columnsCount;
            }
        }

        public void HeaderToExcel(SpreadsheetDocument spreadsheetDocument, ref uint rowNum, uint column)
        {
            //вывод общего заголовка
            if (spreadsheetDocument != null)
            {
                uint styleIndex = OpenXmlRoutines.CreateStyle(spreadsheetDocument, "Arial Narrow", 11, false, PatternValues.None, "0", true, HorizontalAlignmentValues.Center, VerticalAlignmentValues.Bottom, typeof(string));
                Cell cell = OpenXmlRoutines.SetCellValue(spreadsheetDocument, rowNum, column, "Норма");
                cell.StyleIndex = styleIndex;
                rowNum++;
                column--;

                cell = OpenXmlRoutines.SetCellValue(spreadsheetDocument, rowNum, column, "№ ППЭ");
                cell.StyleIndex = styleIndex;
                column++;

                cell = OpenXmlRoutines.SetCellValue(spreadsheetDocument, rowNum, column, "Класс");
                cell.StyleIndex = styleIndex;
                column++;
            }
        }

        public void StatusHeaderToExcel(SpreadsheetDocument spreadsheetDocument, uint rowNum, uint column)
        {
            //вывод наименований столбцов статуса и причины
            if (spreadsheetDocument != null)
            {
                uint styleIndex = OpenXmlRoutines.CreateStyle(spreadsheetDocument, "Arial Narrow", 11, false, PatternValues.None, "0", true, HorizontalAlignmentValues.Center, VerticalAlignmentValues.Bottom, typeof(string));
                Cell cell = OpenXmlRoutines.SetCellValue(spreadsheetDocument, rowNum, column, "Статус");
                cell.StyleIndex = styleIndex;
                column++;

                cell = OpenXmlRoutines.SetCellValue(spreadsheetDocument, rowNum, column, "Код НП");
                cell.StyleIndex = styleIndex;
            }
        }

        public void IdentityToExcel(SpreadsheetDocument spreadsheetDocument, uint rowNum, ref uint column)
        {
            //вывод идентификационные данные
            if (spreadsheetDocument != null)
            {
                //выводим идентификационные данные
                uint styleIndex = OpenXmlRoutines.CreateStyle(spreadsheetDocument, "Arial Narrow", 11, false, PatternValues.None, "0", true, HorizontalAlignmentValues.Center, VerticalAlignmentValues.Bottom, typeof(int));
                Cell cell = null;

                if (int.TryParse(this.CodeSimple, out int codeSimple))
                {
                    cell = OpenXmlRoutines.SetCellValue(spreadsheetDocument, rowNum, column, codeSimple);
                    cell.StyleIndex = styleIndex;
                }

                column++;

                if (int.TryParse(this.DeviceClass, out int deviceClass))
                {
                    cell = OpenXmlRoutines.SetCellValue(spreadsheetDocument, rowNum, column, deviceClass);
                    cell.StyleIndex = styleIndex;
                }

                column++;
            }
        }

        public void BodyToExcel(SpreadsheetDocument spreadsheetDocument, ListOfCalculatorsMinMax listOfCalculatorsMinMax, uint rowNum, ref uint column)
        {
            //вывод значений условий и параметров
            if (spreadsheetDocument != null)
            {
                uint columnsCount = 0;

                if (this.Row.GetMember(Constants.ConditionsInDataSourceFirstIndex, out object valueConditionsInDataSourceFirstIndex))
                {
                    if (int.TryParse(valueConditionsInDataSourceFirstIndex.ToString(), out int conditionsInDataSourceFirstIndex))
                    {
                        List<string> memberNames = this.Row.GetDynamicMemberNames().ToList();
                        IEnumerable<string> manuallyParameters = memberNames.Where(n => (memberNames.IndexOf(n) >= conditionsInDataSourceFirstIndex) && n.Contains("manuallyparameters") && !n.Contains(Constants.HiddenMarker));

                        List<string> names = Constants.OrderedColumnNamesInReport.ToList();
                        names.AddRange(manuallyParameters.Select(n => n.Substring(n.IndexOf(SCME.Common.Constants.FromXMLNameSeparator) + SCME.Common.Constants.FromXMLNameSeparator.Length)));

                        foreach (string name in names)
                        {
                            //вычисляем индекс места хранения в memberNames
                            IEnumerable<string> listOfColumnNames = memberNames.Where(n => (memberNames.IndexOf(n) >= conditionsInDataSourceFirstIndex) && n.Contains(string.Concat(SCME.Common.Constants.FromXMLNameSeparator, name)) && !n.Contains(Constants.HiddenMarker));

                            if (listOfColumnNames != null)
                            {
                                foreach (string columnName in listOfColumnNames)
                                {
                                    int columnIndex = memberNames.IndexOf(columnName);
                                    string trueName = Routines.ParseColumnName(columnName, out string temperatureCondition);

                                    if (trueName != null)
                                    {
                                        //выводим значение условия/параметра
                                        if (this.Row.GetMember(columnName, out object value))
                                        {
                                            if (value != null)
                                            {
                                                uint columnIndexInExcel = column + columnsCount;

                                                //вычисляем значения min/max для определённых в listOfCalculatorsMinMax параметров
                                                string nameOfUnitMeasure = Routines.NameOfUnitMeasure(columnName);
                                                PatternValues patternType = PatternValues.None;
                                                string hexForegroundColor = "0";
                                                bool bold = false;

                                                if (this.Row.GetMember(nameOfUnitMeasure, out object um))
                                                {
                                                    listOfCalculatorsMinMax.Calc(columnIndexInExcel, trueName, um.ToString(), value.ToString());

                                                    //проверяем входит ли выведенное значение параметра в норматив
                                                    if (Routines.IsInNrm(this.Row, columnName) == NrmStatus.Defective)
                                                    {
                                                        //выведенное значение за пределами норм - красим его
                                                        patternType = PatternValues.Solid;
                                                        hexForegroundColor = "FDE2F6";
                                                        bold = true;
                                                    }
                                                    else
                                                    {
                                                        //если выведенное значение принадлежит параметру из списка важных для пользователя параметров - красим его серым
                                                        if (this.Owner.FListOfImportantNamesInReport.Contains(columnName))
                                                        {
                                                            patternType = PatternValues.Solid;
                                                            hexForegroundColor = "EEEEEE";
                                                        }
                                                    }

                                                    Cell cell = null;

                                                    if (Routines.IsInteger(value.ToString(), out int iValue, out bool isDouble, out double dValue))
                                                    {
                                                        //имеем дело с Int
                                                        cell = OpenXmlRoutines.SetCellValue(spreadsheetDocument, rowNum, columnIndexInExcel, iValue);
                                                        cell.StyleIndex = OpenXmlRoutines.CreateStyle(spreadsheetDocument, "Arial Narrow", 11, bold, patternType, hexForegroundColor, true, HorizontalAlignmentValues.Center, VerticalAlignmentValues.Bottom, typeof(int));
                                                    }
                                                    else
                                                    {
                                                        if (isDouble)
                                                        {
                                                            //имеем дело с Double
                                                            cell = OpenXmlRoutines.SetCellValue(spreadsheetDocument, rowNum, columnIndexInExcel, dValue);
                                                            cell.StyleIndex = OpenXmlRoutines.CreateStyle(spreadsheetDocument, "Arial Narrow", 11, bold, patternType, hexForegroundColor, true, HorizontalAlignmentValues.Center, VerticalAlignmentValues.Bottom, typeof(double));
                                                        }
                                                        else
                                                        {
                                                            //имеем дело не с Int и не с Double - со строкой
                                                            cell = OpenXmlRoutines.SetCellValue(spreadsheetDocument, rowNum, columnIndexInExcel, value.ToString());
                                                            cell.StyleIndex = OpenXmlRoutines.CreateStyle(spreadsheetDocument, "Arial Narrow", 11, bold, patternType, hexForegroundColor, true, HorizontalAlignmentValues.Center, VerticalAlignmentValues.Bottom, typeof(string));
                                                        }
                                                    }

                                                    columnsCount++;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                column += columnsCount;
            }
        }

        public uint StatusToExcel(SpreadsheetDocument spreadsheetDocument, uint rowNum, uint column, ref bool? isStatusOK)
        {
            //считываем значение итогового статуса
            string resultStatus = this.Status;

            if (resultStatus == Constants.GoodSatatus)
                isStatusOK = true;
            else
                isStatusOK = (resultStatus == Constants.FaultSatatus) ? false : (bool?)null;

            //в this.CodeOfNonMatch имеем уже слитые в одну строку коды НП
            this.WriteStatusToExcel(spreadsheetDocument, resultStatus, this.CodeOfNonMatch, rowNum, column);

            return column + 1;
        }

        public uint WriteStatusToExcel(SpreadsheetDocument spreadsheetDocument, string status, string codeOfNonMatch, uint rowNum, uint column)
        {
            //вывод значений столбцов статуса и кода НП
            if (spreadsheetDocument != null)
            {
                //выводим статус
                Cell cell = null;
                cell = OpenXmlRoutines.SetCellValue(spreadsheetDocument, rowNum, column, status);
                uint styleStringIndex = OpenXmlRoutines.CreateStyle(spreadsheetDocument, "Arial Narrow", 11, false, PatternValues.None, "0", true, HorizontalAlignmentValues.Center, VerticalAlignmentValues.Bottom, typeof(string));
                cell.StyleIndex = styleStringIndex;
                column++;

                //выводим код НП
                if (int.TryParse(codeOfNonMatch, out int iCodeOfNonMatch))
                {
                    cell = OpenXmlRoutines.SetCellValue(spreadsheetDocument, rowNum, column, iCodeOfNonMatch);
                    cell.StyleIndex = OpenXmlRoutines.CreateStyle(spreadsheetDocument, "Arial Narrow", 11, false, PatternValues.None, "0", true, HorizontalAlignmentValues.Center, VerticalAlignmentValues.Bottom, typeof(int));
                }
                else
                {
                    cell = OpenXmlRoutines.SetCellValue(spreadsheetDocument, rowNum, column, codeOfNonMatch);
                    cell.StyleIndex = styleStringIndex;
                }
            }

            return column;
        }

        public void QtyReleasedByGroupNameToExcel(SpreadsheetDocument spreadsheetDocument, ref uint rowNum, string groupName, int? qtyReleased)
        {
            if (spreadsheetDocument != null)
            {
                uint styleIndex = OpenXmlRoutines.CreateStyle(spreadsheetDocument, "Arial Narrow", 11, false, PatternValues.None, "0", false, HorizontalAlignmentValues.Center, VerticalAlignmentValues.Bottom, typeof(string));

                Cell cell = OpenXmlRoutines.SetCellValue(spreadsheetDocument, rowNum, 2, "Запущено");
                cell.StyleIndex = styleIndex;

                cell = OpenXmlRoutines.SetCellValue(spreadsheetDocument, rowNum, 3, qtyReleased.ToString());
                cell.StyleIndex = styleIndex;

                cell = OpenXmlRoutines.SetCellValue(spreadsheetDocument, rowNum, 4, "шт.");
                cell.StyleIndex = styleIndex;

                rowNum++;
            }
        }

        public void QtyOKFaultToExcel(SpreadsheetDocument spreadsheetDocument, uint rowNum, int totalCount, int statusUnknownCount, int statusFaultCount, int statusOKCount)
        {
            if (spreadsheetDocument != null)
            {
                uint styleStringIndex = OpenXmlRoutines.CreateStyle(spreadsheetDocument, "Arial Narrow", 11, false, PatternValues.None, "0", false, HorizontalAlignmentValues.Center, VerticalAlignmentValues.Bottom, typeof(string));
                uint styleIntIndex = OpenXmlRoutines.CreateStyle(spreadsheetDocument, "Arial Narrow", 11, false, PatternValues.None, "0", false, HorizontalAlignmentValues.Center, VerticalAlignmentValues.Bottom, typeof(int));

                Cell cell = OpenXmlRoutines.SetCellValue(spreadsheetDocument, rowNum, 2, "Кол-во");
                cell.StyleIndex = styleStringIndex;

                cell = OpenXmlRoutines.SetCellValue(spreadsheetDocument, rowNum, 3, totalCount);
                cell.StyleIndex = styleIntIndex;

                cell = OpenXmlRoutines.SetCellValue(spreadsheetDocument, rowNum, 4, "шт.");
                cell.StyleIndex = styleStringIndex;
                rowNum++;

                cell = OpenXmlRoutines.SetCellValue(spreadsheetDocument, rowNum, 2, "Годных");
                cell.StyleIndex = styleStringIndex;

                cell = OpenXmlRoutines.SetCellValue(spreadsheetDocument, rowNum, 3, statusOKCount);
                cell.StyleIndex = styleIntIndex;

                cell = OpenXmlRoutines.SetCellValue(spreadsheetDocument, rowNum, 4, "шт.");
                cell.StyleIndex = styleStringIndex;

                rowNum++;

                cell = OpenXmlRoutines.SetCellValue(spreadsheetDocument, rowNum, 2, "Fault");
                cell.StyleIndex = styleStringIndex;

                cell = OpenXmlRoutines.SetCellValue(spreadsheetDocument, rowNum, 3, statusFaultCount);
                cell.StyleIndex = styleIntIndex;

                cell = OpenXmlRoutines.SetCellValue(spreadsheetDocument, rowNum, 4, "шт.");
                cell.StyleIndex = styleStringIndex;

                rowNum++;

                cell = OpenXmlRoutines.SetCellValue(spreadsheetDocument, rowNum, 2, "Неопределённых");
                cell.StyleIndex = styleStringIndex;

                cell = OpenXmlRoutines.SetCellValue(spreadsheetDocument, rowNum, 3, statusUnknownCount);
                cell.StyleIndex = styleIntIndex;

                cell = OpenXmlRoutines.SetCellValue(spreadsheetDocument, rowNum, 4, "шт.");
                cell.StyleIndex = styleStringIndex;

                rowNum++;

                cell = OpenXmlRoutines.SetCellValue(spreadsheetDocument, rowNum, 2, "Сформир.");
                cell.StyleIndex = styleStringIndex;

                cell = OpenXmlRoutines.SetCellValue(spreadsheetDocument, rowNum, 3, Environment.UserName);
                cell.StyleIndex = styleStringIndex;

                cell = OpenXmlRoutines.SetCellValue(spreadsheetDocument, rowNum, 4, DateTime.Today.ToString("dd.MM.yyyy"));
                cell.StyleIndex = OpenXmlRoutines.CreateStyle(spreadsheetDocument, "Arial Narrow", 11, false, PatternValues.None, "0", false, HorizontalAlignmentValues.Center, VerticalAlignmentValues.Bottom, typeof(DateTime));
            }
        }

        public void ListOfCalculatorsMinMaxToExcel(SpreadsheetDocument spreadsheetDocument, ref uint rowNum, ListOfCalculatorsMinMax listOfCalculatorsMinMax)
        {
            //вывод вычисленных значений min/max из listOfCalculatorMinMax в Excel
            if ((spreadsheetDocument != null) && (listOfCalculatorsMinMax != null))
            {
                foreach (CalculatorMinMax calculator in listOfCalculatorsMinMax)
                {
                    uint? column = calculator.Column;

                    //если из calculator выводить нечего, то ничего не выводим
                    if (column != null)
                    {
                        uint styleStringIndex = OpenXmlRoutines.CreateStyle(spreadsheetDocument, "Arial Narrow", 11, false, PatternValues.None, "0", false, HorizontalAlignmentValues.Center, VerticalAlignmentValues.Bottom, typeof(string));
                        uint styleDoubleIndex = OpenXmlRoutines.CreateStyle(spreadsheetDocument, "Arial Narrow", 11, false, PatternValues.None, "0", false, HorizontalAlignmentValues.Center, VerticalAlignmentValues.Bottom, typeof(double));

                        Cell cell = OpenXmlRoutines.SetCellValue(spreadsheetDocument, rowNum, (uint)column, "min/max");
                        cell.StyleIndex = styleStringIndex;

                        //выводим наименование параметра и его единицу измерения
                        string value = string.Concat(Routines.VtoU(calculator.Name), ", ", calculator.Um);
                        cell = OpenXmlRoutines.SetCellValue(spreadsheetDocument, rowNum + 1, (uint)column, value);
                        cell.StyleIndex = styleStringIndex;

                        //выводим значение минимума
                        if (calculator.MinValue != null)
                        {
                            cell = OpenXmlRoutines.SetCellValue(spreadsheetDocument, rowNum + 2, (uint)column, (double)calculator.MinValue);
                            cell.StyleIndex = styleDoubleIndex;
                        }

                        //выводим значение максимума
                        if (calculator.MaxValue != null)
                        {
                            cell = OpenXmlRoutines.SetCellValue(spreadsheetDocument, rowNum + 3, (uint)column, (double)calculator.MaxValue);
                            cell.StyleIndex = styleDoubleIndex;
                        }
                    }
                }

                rowNum += 4;
            }
        }

        public void AssemblyTopHeaderToExcel(SpreadsheetDocument spreadsheetDocument, string assemblyProtocolDescr, ref uint rowNum)
        {
            //выводим заголовок отчёта
            if (spreadsheetDocument != null)
            {
                uint column = 1;
                //выводим обозначение протокола сборки
                string value = string.Format("ПРОТОКОЛ СБОРКИ {0}", assemblyProtocolDescr);
                uint styleIndex = OpenXmlRoutines.CreateStyle(spreadsheetDocument, "Arial Narrow", 11, false, PatternValues.None, "0", true, HorizontalAlignmentValues.Center, VerticalAlignmentValues.Bottom, typeof(string));
                OpenXmlRoutines.MergeCells(spreadsheetDocument, rowNum, column, rowNum, column + 2, value, styleIndex);

                column = 4;
                value = "№ ПЗ";
                OpenXmlRoutines.MergeCells(spreadsheetDocument, rowNum, column, rowNum, column + 1, value, styleIndex);

                column = 6;
                OpenXmlRoutines.MergeCells(spreadsheetDocument, rowNum, column, rowNum, column + 2, this.AssemblyJob, styleIndex);

                column = 9;
                value = "Тип корп.";
                OpenXmlRoutines.MergeCells(spreadsheetDocument, rowNum, column, rowNum, column + 1, value, styleIndex);

                value = "КОФ";
                OpenXmlRoutines.MergeCells(spreadsheetDocument, rowNum + 1, column, rowNum + 1, column + 1, value, styleIndex);

                column = 11;
                OpenXmlRoutines.MergeCells(spreadsheetDocument, rowNum, column, rowNum, column + 1, this.PackageType, styleIndex);

                Cell cell = OpenXmlRoutines.SetCellValue(spreadsheetDocument, rowNum + 1, column, this.Omnity);
                cell.StyleIndex = styleIndex;

                //считываем установленный пользователем тип изделия для данного протокола сборки
                //он нужен для того, чтобы определить сколько столбцов занять надписью "Сформировал"
                string deviceTypeRu = this.Owner[0].DeviceTypeRu;

                //для типов, начинающихся с m (англ.) или м (русск.) надо занять 3 столбца, для любых других типов 2 столбца
                uint columnsToAdd;

                if (string.IsNullOrEmpty(deviceTypeRu))
                {
                    columnsToAdd = 1;
                }
                else
                    columnsToAdd = this.IsDeviceTypeRuMStarting(deviceTypeRu) ? (uint)2 : (uint)1;

                column = 13;
                value = "Сформировал";
                OpenXmlRoutines.MergeCells(spreadsheetDocument, rowNum, column, rowNum, column + columnsToAdd, value, styleIndex);

                value = ((MainWindow)System.Windows.Application.Current.MainWindow).TabNum;
                cell = OpenXmlRoutines.SetCellValue(spreadsheetDocument, rowNum + 1, column, value);
                cell.StyleIndex = styleIndex;

                if (DbRoutines.FullUserNameByUserID(((MainWindow)System.Windows.Application.Current.MainWindow).FUserID, out string fullUserName))
                    OpenXmlRoutines.MergeCells(spreadsheetDocument, rowNum + 1, column + 1, rowNum + 1, column + 2, fullUserName, styleIndex);

                //вторая строка шапки
                rowNum++;
                column = 1;
                value = "Дата формирования";
                OpenXmlRoutines.MergeCells(spreadsheetDocument, rowNum, column, rowNum, column + 2, value, styleIndex);

                column = 4;
                DateTime dtValue = DateTime.Today;
                uint dtStyleIndex = OpenXmlRoutines.CreateStyle(spreadsheetDocument, "Arial Narrow", 11, false, PatternValues.None, "0", true, HorizontalAlignmentValues.Center, VerticalAlignmentValues.Bottom, typeof(DateTime));
                OpenXmlRoutines.MergeCells(spreadsheetDocument, rowNum, column, rowNum, column + 2, dtValue, dtStyleIndex);

                //третья строка шапки
                rowNum++;
                column = 1;
                value = "Изделие";
                OpenXmlRoutines.MergeCells(spreadsheetDocument, rowNum, column, rowNum, column + 2, value, styleIndex);

                column = 4;
                OpenXmlRoutines.MergeCells(spreadsheetDocument, rowNum, column, rowNum, column + 3, this.Device, styleIndex);

                rowNum++;
            }
        }

        public void TableColumnNamesToExcel(SpreadsheetDocument spreadsheetDocument, List<NameDescr> columNames, uint rowNum, out uint? firstUserDataColumn, out uint? lastUserDataColumn, out uint? lastColumn)
        {
            firstUserDataColumn = null;
            lastUserDataColumn = null;
            lastColumn = null;

            if ((spreadsheetDocument != null) && (columNames != null))
            {
                //чтобы узнать номер последнего столбца занятого под параметры ППЭ - сначала выводим наименования параметров ППЭ
                uint startRowNum = rowNum;
                rowNum++;

                string value;
                Cell cell;
                uint styleIndex;

                uint column = 4;
                uint columnIndexInExcel = column;
                uint? columnsCount = null;

                //сначала выводим содержимое принятого columNames
                foreach (NameDescr nameDescr in columNames)
                {
                    //выводим температурный режим при котором выполнено измерение данного параметра
                    columnsCount = (columnsCount == null) ? 0 : columnsCount + 1;

                    columnIndexInExcel = column + (uint)columnsCount;

                    value = string.Concat(nameDescr.TemperatureCondition.ToString(), " ", nameDescr.TemperatureValue);
                    cell = OpenXmlRoutines.SetCellValue(spreadsheetDocument, rowNum, columnIndexInExcel, value);
                    cell.StyleIndex = OpenXmlRoutines.CreateStyle(spreadsheetDocument, "Arial Narrow", 11, false, PatternValues.None, "0", true, HorizontalAlignmentValues.Center, VerticalAlignmentValues.Bottom, typeof(string));

                    //выводим имя условия/параметра
                    value = this.ColumnName(nameDescr.Name, nameDescr.Subject, nameDescr.TemperatureCondition);
                    cell = OpenXmlRoutines.SetCellValue(spreadsheetDocument, rowNum + 1, columnIndexInExcel, value);
                    cell.StyleIndex = OpenXmlRoutines.CreateStyle(spreadsheetDocument, "Arial Narrow", 11, true, PatternValues.None, "0", true, HorizontalAlignmentValues.Center, VerticalAlignmentValues.Bottom, typeof(string));

                    //выводим единицу измерения
                    cell = OpenXmlRoutines.SetCellValue(spreadsheetDocument, rowNum + 2, columnIndexInExcel, nameDescr.UnitMeasure);
                    cell.StyleIndex = OpenXmlRoutines.CreateStyle(spreadsheetDocument, "Arial Narrow", 11, false, PatternValues.None, "0", true, HorizontalAlignmentValues.Center, VerticalAlignmentValues.Bottom, typeof(string));

                    //выводим описание норм
                    cell = OpenXmlRoutines.SetCellValue(spreadsheetDocument, rowNum + 3, columnIndexInExcel, nameDescr.Nrm);
                    cell.StyleIndex = OpenXmlRoutines.CreateStyle(spreadsheetDocument, "Arial Narrow", 11, true, PatternValues.None, "0", true, HorizontalAlignmentValues.Center, VerticalAlignmentValues.Bottom, typeof(string));
                }

                if (columnsCount != 0)
                {
                    value = "Параметры на ППЭ";
                    styleIndex = OpenXmlRoutines.CreateStyle(spreadsheetDocument, "Arial Narrow", 11, false, PatternValues.None, "0", true, HorizontalAlignmentValues.Center, VerticalAlignmentValues.Bottom, typeof(string));
                    OpenXmlRoutines.MergeCells(spreadsheetDocument, startRowNum, column, startRowNum, column + (uint)columnsCount, value, styleIndex);
                }

                //выводим список имён параметров которые пользователь будет заполнять руками на распечанном отчёте
                //считываем установленный пользователем тип изделия для данного протокола сборки
                string deviceTypeRu = this.Owner[0].DeviceTypeRu;

                if (deviceTypeRu != null)
                {
                    //по считанному типу изделия получаем список имён параметров/условий
                    List<SCME.dbViewer.Routines.ParamConditionDescr> listOfNames = Routines.AssemblyProtocolEmptyNamesByDeviceTypeRu(deviceTypeRu);

                    if (listOfNames != null)
                    {
                        columnIndexInExcel++;

                        //запоминаем первый столбец данных которые пользователь будет вводить руками
                        firstUserDataColumn = columnIndexInExcel;

                        columnsCount = null;

                        foreach (SCME.dbViewer.Routines.ParamConditionDescr name in listOfNames)
                        {
                            columnsCount = (columnsCount == null) ? 0 : columnsCount + 1;

                            cell = OpenXmlRoutines.SetCellValue(spreadsheetDocument, rowNum, columnIndexInExcel + (uint)columnsCount, name.TemperatureMode);
                            cell.StyleIndex = OpenXmlRoutines.CreateStyle(spreadsheetDocument, "Arial Narrow", 11, false, PatternValues.None, "0", true, HorizontalAlignmentValues.Center, VerticalAlignmentValues.Bottom, typeof(string));

                            cell = OpenXmlRoutines.SetCellValue(spreadsheetDocument, rowNum + 1, columnIndexInExcel + (uint)columnsCount, name.Descr);
                            cell.StyleIndex = OpenXmlRoutines.CreateStyle(spreadsheetDocument, "Arial Narrow", 11, true, PatternValues.None, "0", true, HorizontalAlignmentValues.Center, VerticalAlignmentValues.Bottom, typeof(string));

                            cell = OpenXmlRoutines.SetCellValue(spreadsheetDocument, rowNum + 2, columnIndexInExcel + (uint)columnsCount, name.UnitMeasure);
                            cell.StyleIndex = OpenXmlRoutines.CreateStyle(spreadsheetDocument, "Arial Narrow", 11, false, PatternValues.None, "0", true, HorizontalAlignmentValues.Center, VerticalAlignmentValues.Bottom, typeof(string));
                        }

                        lastUserDataColumn = columnIndexInExcel + columnsCount;

                        value = "№ прибора";
                        styleIndex = OpenXmlRoutines.CreateStyle(spreadsheetDocument, "Arial Narrow", 11, true, PatternValues.None, "0", true, HorizontalAlignmentValues.Center, VerticalAlignmentValues.Center, typeof(string));
                        OpenXmlRoutines.MergeCells(spreadsheetDocument, startRowNum, columnIndexInExcel + (uint)columnsCount + 1, startRowNum + 4, columnIndexInExcel + (uint)columnsCount + 1, value, styleIndex);

                        //здесь будем учитывать количество добавляемых столбцов в зависимости от типа изделия
                        uint additionColumnsByDeviceType = 0;

                        //определяем надо ли формировать столбец для ручного ввода данных с именем Visol
                        if (this.IsDeviceTypeRuMStarting(deviceTypeRu))
                        {
                            value = "Visol";
                            styleIndex = OpenXmlRoutines.CreateStyle(spreadsheetDocument, "Arial Narrow", 11, true, PatternValues.None, "0", true, HorizontalAlignmentValues.Center, VerticalAlignmentValues.Center, typeof(string));
                            OpenXmlRoutines.MergeCells(spreadsheetDocument, startRowNum, columnIndexInExcel + (uint)columnsCount + 2, startRowNum + 4, columnIndexInExcel + (uint)columnsCount + 2, value, styleIndex);

                            additionColumnsByDeviceType = 1;
                        }

                        value = "Примечания";
                        styleIndex = OpenXmlRoutines.CreateStyle(spreadsheetDocument, "Arial Narrow", 11, true, PatternValues.None, "0", true, HorizontalAlignmentValues.Center, VerticalAlignmentValues.Center, typeof(string));
                        OpenXmlRoutines.MergeCells(spreadsheetDocument, startRowNum, columnIndexInExcel + (uint)columnsCount + 2 + additionColumnsByDeviceType, startRowNum + 4, columnIndexInExcel + (uint)columnsCount + 2 + additionColumnsByDeviceType, value, styleIndex);

                        //возвращаем номер последнего выведенного столбца
                        lastColumn = columnIndexInExcel + columnsCount + 2 + additionColumnsByDeviceType;
                    }
                }

                value = "Параметры на СПП";
                styleIndex = OpenXmlRoutines.CreateStyle(spreadsheetDocument, "Arial Narrow", 11, false, PatternValues.None, "0", true, HorizontalAlignmentValues.Center, VerticalAlignmentValues.Center, typeof(string));
                OpenXmlRoutines.MergeCells(spreadsheetDocument, startRowNum, columnIndexInExcel, startRowNum, columnIndexInExcel + (uint)columnsCount, value, styleIndex);

                //создаём пустые ячейки с очерченными границами в строке где выводятся нормы, но в столбцах отведённых под ручное написание параметров на СПП                
                for (uint columnIndex = columnIndexInExcel; columnIndex <= columnIndexInExcel + (uint)columnsCount; columnIndex++)
                {
                    cell = OpenXmlRoutines.SetCellValue(spreadsheetDocument, rowNum + 3, columnIndex, string.Empty);
                    cell.StyleIndex = styleIndex;
                }

                rowNum = 6;
                column = 1;

                value = "№ п/п";
                styleIndex = OpenXmlRoutines.CreateStyle(spreadsheetDocument, "Arial Narrow", 11, false, PatternValues.None, "0", true, HorizontalAlignmentValues.Center, VerticalAlignmentValues.Center, typeof(string));
                OpenXmlRoutines.MergeCells(spreadsheetDocument, rowNum, column, rowNum + 2, column, value, styleIndex);

                styleIndex = OpenXmlRoutines.CreateStyle(spreadsheetDocument, "Arial Narrow", 11, false, PatternValues.None, "0", true, HorizontalAlignmentValues.Center, VerticalAlignmentValues.Center, typeof(string));
                OpenXmlRoutines.MergeCells(spreadsheetDocument, rowNum, column + 1, rowNum + 2, column + 1, Properties.Resources.Code, styleIndex);

                value = "ПЗ ППЭ";
                styleIndex = OpenXmlRoutines.CreateStyle(spreadsheetDocument, "Arial Narrow", 11, false, PatternValues.None, "0", true, HorizontalAlignmentValues.Center, VerticalAlignmentValues.Center, typeof(string));
                OpenXmlRoutines.MergeCells(spreadsheetDocument, rowNum, column + 2, rowNum + 2, column + 2, value, styleIndex);
            }
        }

        public void TableIdentityDataToExcel(SpreadsheetDocument spreadsheetDocument, int counter, uint rowNum)
        {
            if (spreadsheetDocument != null)
            {
                //выводим порядковый номер
                uint column = 1;
                Cell cell = OpenXmlRoutines.SetCellValue(spreadsheetDocument, rowNum, column, counter);
                cell.StyleIndex = OpenXmlRoutines.CreateStyle(spreadsheetDocument, "Arial Narrow", 11, false, PatternValues.None, "0", true, HorizontalAlignmentValues.Center, VerticalAlignmentValues.Bottom, typeof(int));

                //выводим номер ППЭ
                column = 2;
                cell = OpenXmlRoutines.SetCellValue(spreadsheetDocument, rowNum, column, this.CodeSimple);
                cell.StyleIndex = OpenXmlRoutines.CreateStyle(spreadsheetDocument, "Arial Narrow", 11, false, PatternValues.None, "0", true, HorizontalAlignmentValues.Center, VerticalAlignmentValues.Bottom, typeof(string));

                //выводим код ПЗ извлечённый из обозначения ППЭ
                column = 3;
                cell = OpenXmlRoutines.SetCellValue(spreadsheetDocument, rowNum, column, this.JobFromCode);
                cell.StyleIndex = OpenXmlRoutines.CreateStyle(spreadsheetDocument, "Arial Narrow", 11, false, PatternValues.None, "0", true, HorizontalAlignmentValues.Center, VerticalAlignmentValues.Bottom, typeof(string));

                //номер прибора пользователь заполняет от руки в распечатанном отчёте
            }
        }

        public void TableBodyToExcel(SpreadsheetDocument spreadsheetDocument, List<NameDescr> columNames, uint rowNum)
        {
            //вывод значений условий и параметров
            if ((spreadsheetDocument != null) && (columNames != null))
            {
                uint column = 4;
                uint columnsCount = 0;

                //идём по списку columNames - это именно те столбцы, которые на данный момент уже выведены в шапке таблицы Excel
                foreach (NameDescr nameDescr in columNames)
                {
                    //формируем имя столбца параметра/условия и считываем его значение
                    string name = Routines.ColumnNameInDataSource(nameDescr.TemperatureCondition.ToString(), nameDescr.Test, nameDescr.Subject, nameDescr.Name);

                    string temperatureValue = this.TemperatureByColumnName(name);

                    if (temperatureValue == nameDescr.TemperatureValue)
                    {
                        bool valueRetrived = false;
                        object value = null;

                        //нормы на параметры созданные вручную в разных записях могут быть разными т.к. в этих записях могут быть разные профили
                        //нормы на параметры измеренные на оборудовании в отчёте по протоколу сборки считываются из справочника норм и зависят от: itav, deviceTypeID, constructive, modification, и эти нормы не хранятся в анализируемой записи
                        switch (nameDescr.Subject)
                        {
                            case Common.Routines.XMLValues.ManuallyParameters:
                                string nrm = this.NrmByColumnName(name);

                                if (nrm == nameDescr.Nrm)
                                    valueRetrived = this.Row.GetMember(name, out value);

                                break;

                            default:
                                valueRetrived = this.Row.GetMember(name, out value);
                                break;
                        }

                        if (valueRetrived)
                        {
                            uint columnIndexInExcel = column + columnsCount;
                            Cell cell;
                            Type type;

                            if (Routines.IsInteger(value.ToString(), out int iValue, out bool isDouble, out double dValue))
                            {
                                //имеем дело с Int
                                cell = OpenXmlRoutines.SetCellValue(spreadsheetDocument, rowNum, columnIndexInExcel, iValue);
                                type = typeof(int);
                            }
                            else
                            {
                                if (isDouble)
                                {
                                    //имеем дело с Double
                                    cell = OpenXmlRoutines.SetCellValue(spreadsheetDocument, rowNum, columnIndexInExcel, dValue);
                                    type = typeof(double);
                                }
                                else
                                {
                                    //имеем дело не с Int и не с Double - со строкой
                                    cell = OpenXmlRoutines.SetCellValue(spreadsheetDocument, rowNum, columnIndexInExcel, value.ToString());
                                    type = typeof(string);
                                }
                            }

                            //проверяем входит ли выведенное значение параметра в норматив
                            if (Routines.IsInNrm(this.Row, name) == NrmStatus.Defective)
                            {
                                //выведенное значение за пределами норм - красим его
                                cell.StyleIndex = OpenXmlRoutines.CreateStyle(spreadsheetDocument, "Arial Narrow", 11, true, PatternValues.Solid, "FF0000", true, HorizontalAlignmentValues.Center, VerticalAlignmentValues.Bottom, type);
                            }
                            else
                            {
                                //если выведенное значение принадлежит параметру из списка важных для пользователя параметров - красим его серым
                                if (this.Owner.FListOfImportantNamesInReport.Contains(name))
                                {
                                    cell.StyleIndex = OpenXmlRoutines.CreateStyle(spreadsheetDocument, "Arial Narrow", 11, false, PatternValues.Solid, "EEEEEE", true, HorizontalAlignmentValues.Center, VerticalAlignmentValues.Bottom, type);
                                }
                                else
                                    cell.StyleIndex = OpenXmlRoutines.CreateStyle(spreadsheetDocument, "Arial Narrow", 11, false, PatternValues.None, "0", true, HorizontalAlignmentValues.Center, VerticalAlignmentValues.Bottom, type);
                            }
                        }
                    }

                    columnsCount++;
                }
            }
        }

        public void MakeEmptyCells(SpreadsheetDocument spreadsheetDocument, uint rowNum, uint startColumn, uint endColumn)
        {
            //вывод пустых ячеек в области параметров на СПП, которую пользователь заполняет руками

            if (spreadsheetDocument != null)
            {
                Cell cell;
                uint styleIndex = OpenXmlRoutines.CreateStyle(spreadsheetDocument, "Arial Narrow", 11, false, PatternValues.None, "0", true, HorizontalAlignmentValues.Center, VerticalAlignmentValues.Center, typeof(string));

                for (uint columnIndex = startColumn; columnIndex <= endColumn; columnIndex++)
                {
                    cell = OpenXmlRoutines.SetCellValue(spreadsheetDocument, rowNum, columnIndex, string.Empty);
                    cell.StyleIndex = styleIndex;
                }
            }
        }

        public void DeviceCommentsToExcel(SpreadsheetDocument spreadsheetDocument, uint rowNum, uint columnDeviceComments)
        {
            //вывод значения поля DeviceComments
            if (this.Row.GetMember(Common.Constants.DeviceComments, out object value))
            {
                if (value != null)
                {
                    Cell cell = OpenXmlRoutines.SetCellValue(spreadsheetDocument, rowNum, columnDeviceComments, value.ToString());
                    cell.StyleIndex = OpenXmlRoutines.CreateStyle(spreadsheetDocument, "Arial Narrow", 11, false, PatternValues.None, "0", true, HorizontalAlignmentValues.Left, VerticalAlignmentValues.Center, typeof(string));
                }
            }
        }

        public void SetColumnWidth(SpreadsheetDocument spreadsheetDocument, uint firstUserColumn, uint lastColumn, double? rowHeight)
        {
            if (spreadsheetDocument != null)
            {
                Columns columns = OpenXmlRoutines.Columns(spreadsheetDocument);

                if (columns.ChildElements.Count() != 0)
                    columns.RemoveAllChildren();

                Dictionary<uint, double> maxWidthColumnsInPixel = OpenXmlRoutines.MaxWidthColumnsInPixel(spreadsheetDocument, rowHeight);

                //формируем описание столбцов в columns - устанавливаем ширину столбца
                foreach (KeyValuePair<uint, double> entry in maxWidthColumnsInPixel)
                {
                    switch ((entry.Key >= firstUserColumn) && (entry.Key <= lastColumn))
                    {
                        case true:
                            //устанавливаем ширину столбцов равной фиксированному значению, ибо эти столбцы всегда пустые и пользователь вносит туда данные вручную
                            columns.Append(new Column() { Min = DocumentFormat.OpenXml.UInt32Value.FromUInt32(entry.Key), Max = DocumentFormat.OpenXml.UInt32Value.FromUInt32(entry.Key), Width = DocumentFormat.OpenXml.DoubleValue.FromDouble(8), CustomWidth = DocumentFormat.OpenXml.BooleanValue.FromBoolean(true), BestFit = DocumentFormat.OpenXml.BooleanValue.FromBoolean(true) });
                            break;

                        default:
                            //устанавливаем значение ширины столбцов по хранящемуся в maxWidthColumnsInPixel значению
                            //maxWidthColumnsInPixel хранит значения которые перед применением необходимо пересчитать
                            columns.Append(new Column() { Min = DocumentFormat.OpenXml.UInt32Value.FromUInt32(entry.Key), Max = DocumentFormat.OpenXml.UInt32Value.FromUInt32(entry.Key), Width = OpenXmlRoutines.WidthToOpenXml(entry.Value), CustomWidth = DocumentFormat.OpenXml.BooleanValue.FromBoolean(true), BestFit = DocumentFormat.OpenXml.BooleanValue.FromBoolean(true) });
                            break;
                    }
                }
            }
        }

        public void BottomDataToExcel(SpreadsheetDocument spreadsheetDocument, List<ParamReference> norms, uint rowNum, uint lastColumn)
        {
            //выводим данные из записи с индексом 0 под выведенными данными
            if ((spreadsheetDocument != null) && (norms != null))
            {
                uint column = 2;

                //левый столбец
                uint rowNumOffSet = 0;
                Cell cell;

                //сначала выводим описание норм
                string value = "Предельно допустимые значения параметров:";
                uint styleIndex = OpenXmlRoutines.CreateStyle(spreadsheetDocument, "Arial Narrow", 11, true, PatternValues.None, "0", false, HorizontalAlignmentValues.Center, VerticalAlignmentValues.Bottom, typeof(string));
                OpenXmlRoutines.MergeCells(spreadsheetDocument, rowNum, column, rowNum, column + 5, value, styleIndex);

                rowNumOffSet++;

                uint dataStyleIndex = OpenXmlRoutines.CreateStyle(spreadsheetDocument, "Arial Narrow", 11, false, PatternValues.None, "0", false, HorizontalAlignmentValues.Left, VerticalAlignmentValues.Bottom, typeof(string));

                if (!string.IsNullOrEmpty(this.Tq))
                {
                    value = string.Format("{0}≤{1} {2}", Constants.Tq, this.Tq, "мкс");
                    cell = OpenXmlRoutines.SetCellValue(spreadsheetDocument, rowNum + rowNumOffSet, column, value);
                    cell.StyleIndex = dataStyleIndex;

                    rowNumOffSet++;
                }

                if (!string.IsNullOrEmpty(this.Trr))
                {
                    value = string.Format("{0}≤{1} {2}", Constants.Trr, this.Trr, "мкс");
                    cell = OpenXmlRoutines.SetCellValue(spreadsheetDocument, rowNum + rowNumOffSet, column, value);
                    cell.StyleIndex = dataStyleIndex;

                    rowNumOffSet++;
                }

                if (!string.IsNullOrEmpty(this.Qrr))
                {
                    value = string.Format("{0}≤{1} {2}", Constants.Qrr, this.Qrr, "мкКл");
                    cell = OpenXmlRoutines.SetCellValue(spreadsheetDocument, rowNum + rowNumOffSet, column, value);
                    cell.StyleIndex = dataStyleIndex;

                    rowNumOffSet++;
                }

                if (!string.IsNullOrEmpty(this.DUdt))
                {
                    value = string.Format("{0}≥{1} {2}", Common.Constants.DUdt, this.DUdt, this.DUdtUnitMeasure);
                    cell = OpenXmlRoutines.SetCellValue(spreadsheetDocument, rowNum + rowNumOffSet, column, value);
                    cell.StyleIndex = dataStyleIndex;

                    rowNumOffSet++;
                }

                if (!string.IsNullOrEmpty(this.Tgt))
                {
                    value = string.Format("{0}≤{1} {2}", Constants.Tgt, this.Tgt, "мкс");
                    cell = OpenXmlRoutines.SetCellValue(spreadsheetDocument, rowNum + rowNumOffSet, column, value);
                    cell.StyleIndex = dataStyleIndex;

                    rowNumOffSet++;
                }

                //выводим нормы из справочника DEVICEREFERENCEID на IGT
                ParamReference paramReference = norms.Where(item => item.Name == "IGT").FirstOrDefault();
                if (paramReference != null)
                {
                    value = string.Format("IgtMax={0} {1}", paramReference.MaxValue, "мА");
                    cell = OpenXmlRoutines.SetCellValue(spreadsheetDocument, rowNum + rowNumOffSet, column, value);
                    cell.StyleIndex = dataStyleIndex;

                    rowNumOffSet++;
                }

                //выводим нормы из справочника DEVICEREFERENCEID на UGT
                paramReference = norms.Where(item => item.Name == "UGT").FirstOrDefault();
                if (paramReference != null)
                {
                    value = string.Format("UgtMax={0} {1}", paramReference.MaxValue, "В");
                    cell = OpenXmlRoutines.SetCellValue(spreadsheetDocument, rowNum + rowNumOffSet, column, value);
                    cell.StyleIndex = dataStyleIndex;

                    rowNumOffSet++;
                }

                value = "Статистика:";
                OpenXmlRoutines.MergeCells(spreadsheetDocument, rowNum + rowNumOffSet, column, rowNum + rowNumOffSet, column + 5, value, styleIndex);

                rowNumOffSet++;

                if ((this.IgtMin != 0) || (this.IgtMax != 0))
                {
                    value = string.Format("{0}{1}={2} {3}, {4}{5}={6} {7}", Constants.Igt, Constants.Min, this.IgtMin, this.IgtUnitMeasure, Constants.Igt, Constants.Max, this.IgtMax, this.IgtUnitMeasure);
                    cell = OpenXmlRoutines.SetCellValue(spreadsheetDocument, rowNum + rowNumOffSet, column, value);
                    cell.StyleIndex = dataStyleIndex;

                    rowNumOffSet++;
                }

                if ((this.UgtMin != 0) || (this.UgtMax != 0))
                {
                    value = string.Format("{0}{1}={2} {3}, {4}{5}={6} {7}", Constants.Ugt, Constants.Min, this.UgtMin, this.UgtUnitMeasure, Constants.Ugt, Constants.Max, this.UgtMax, this.UgtUnitMeasure);
                    cell = OpenXmlRoutines.SetCellValue(spreadsheetDocument, rowNum + rowNumOffSet, column, value);
                    cell.StyleIndex = dataStyleIndex;

                    rowNumOffSet++;
                }

                //выводим количество записей в отчёте
                column = lastColumn - 7;
                value = "Количество";
                cell = OpenXmlRoutines.SetCellValue(spreadsheetDocument, rowNum, column, value);
                cell.StyleIndex = dataStyleIndex;

                uint dataCenterStyleIndex = OpenXmlRoutines.CreateStyle(spreadsheetDocument, "Arial Narrow", 11, false, PatternValues.None, "0", false, HorizontalAlignmentValues.Center, VerticalAlignmentValues.Bottom, typeof(string));
                cell = OpenXmlRoutines.SetCellValue(spreadsheetDocument, rowNum, column + 6, this.AssemblyReportRecordCount);
                cell.StyleIndex = dataCenterStyleIndex;

                value = "шт.";
                cell = OpenXmlRoutines.SetCellValue(spreadsheetDocument, rowNum, column + 7, value);
                cell.StyleIndex = dataCenterStyleIndex;

                //выводим запущенное по ПЗ количество изделий
                value = "Общее количество в ПЗ";
                cell = OpenXmlRoutines.SetCellValue(spreadsheetDocument, rowNum + 1, column, value);
                cell.StyleIndex = dataStyleIndex;

                string qtyReleasedByGroupName = this.QtyReleasedByGroupName?.ToString();
                if (qtyReleasedByGroupName != null)
                {
                    cell = OpenXmlRoutines.SetCellValue(spreadsheetDocument, rowNum + 1, column + 6, qtyReleasedByGroupName);
                    cell.StyleIndex = dataCenterStyleIndex;

                    value = "шт.";
                    cell = OpenXmlRoutines.SetCellValue(spreadsheetDocument, rowNum + 1, column + 7, value);
                    cell.StyleIndex = dataCenterStyleIndex;
                }
            }
        }

        public void PageSetup(SpreadsheetDocument spreadsheetDocument)
        {
            if (spreadsheetDocument != null)
            {
                WorksheetPart worksheetPart = spreadsheetDocument.WorkbookPart.GetPartsOfType<WorksheetPart>().First();

                if (worksheetPart != null)
                {
                    DocumentFormat.OpenXml.Spreadsheet.Worksheet worksheet = worksheetPart.Worksheet;

                    if (worksheet != null)
                    {
                        SheetProperties sheetProperties = new SheetProperties(new PageSetupProperties());
                        sheetProperties.PageSetupProperties.FitToPage = DocumentFormat.OpenXml.BooleanValue.FromBoolean(true);
                        worksheet.SheetProperties = sheetProperties;

                        //PageMargins должен быть добавлен после SheetData, но перед PageSetup
                        PageMargins pageMargins = new PageMargins()
                        {
                            Left = 0.196850393700787,
                            Right = 0.196850393700787,
                            Top = 0.393700787401575,
                            Bottom = 0.393700787401575,
                            Header = 0,
                            Footer = 0
                        };
                        worksheet.AppendChild(pageMargins);

                        DocumentFormat.OpenXml.Spreadsheet.PageSetup pageSetup = new DocumentFormat.OpenXml.Spreadsheet.PageSetup()
                        {
                            Orientation = OrientationValues.Landscape,
                            PaperSize = 9 //A4
                        };
                        worksheet.AppendChild(pageSetup);
                    }
                }
            }
        }
    }

    public class ReportData : List<ReportRecord>
    {
        //private Excel.Application exelApp = null;
        //private Excel.Worksheet sheet = null;

        //значение первого температурного режима в формируемом отчёте
        //public string TC1 = null;
        //public ListOfBannedNamesForUseInReport FListOfBannedNamesForUseInReport = new ListOfBannedNamesForUseInReport();
        public ListOfImportantNamesInReport FListOfImportantNamesInReport = new ListOfImportantNamesInReport();

        public ReportData()
        {
        }

        public ReportData(List<DynamicObj> source)
        {
            //запоминаем ссылки на каждую запись принятого source
            foreach (DynamicObj row in source)
            {
                ReportRecord item = new ReportRecord(this, row);
                this.Add(item);
            }
        }

        public ReportData(List<ReportRecord> source) : base(source)
        {
        }

        private string CheckAssemblyProtocolIDAndSapID(DynamicObj row)
        {
            //проверка значений полей ASSEMBLYPROTOCOLID и SAPID в принятой записи row
            //принятая row есть результат группировки результатов измерений: row содержит в себе данные нескольких результатов измерений (может быть только один результат, но частный случай) 
            //простой пример: сегодня выполнили измерения параметров при температуре RT, по ним сформировали протокол сборки
            //                спустя какое-то время выполнили измерения параметров при температуре TM
            //                при построении отчёта по протоколу сборки система будет использовать автоматически сгруппированный результат RT-TM, хотя пользователь руками не включал в ранее сформированный протокол сборки измерение TM.
            //при формировании списка значений ASSEMBLYPROTOCOLID значения NULL заменены на 'EMPTY' (см. реализацию функции GroupData в БД)
            //данная реализация проверяет значения полей SAPID и ASSEMBLYPROTOCOLID принятой записи row и если:
            //((ASSEMBLYPROTOCOLID='EMPTY') && (SAPID=0))  - устанавливает в базе данных ASSEMBLYPROTOCOLID и SAPID=1;
            //((ASSEMBLYPROTOCOLID='EMPTY') && (SAPID=1))  - ругается;
            //((ASSEMBLYPROTOCOLID!='EMPTY') && (SAPID=0)) - ругается;
            //((ASSEMBLYPROTOCOLID!='EMPTY') && (SAPID=1)) - нормальная ситуация
            //((ASSEMBLYPROTOCOLID!='EMPTY') и номер не совпадает с номером протокола сборки && (SAPID=1)) - ругается;
            if (row != null)
            {
                //считываем множество значений DEV_ID
                IEnumerable<string> devIDList = null;
                if (row.GetMember(Common.Constants.DevID, out object objDevID))
                    devIDList = objDevID.ToString().Split(new string[] { Common.Constants.cString_AggDelimeter.ToString() }, StringSplitOptions.None);

                //считываем множество значений  ASSEMBLYPROTOCOLID
                IEnumerable<string> assemblyProtocolIDList = null;
                if (row.GetMember(Common.Constants.AssemblyProtocolID, out object objAssemblyProtocolID))
                    assemblyProtocolIDList = objAssemblyProtocolID.ToString().Split(new string[] { Common.Constants.cString_AggDelimeter.ToString() }, StringSplitOptions.None);

                //считываем множество значений SAPID
                IEnumerable<string> sapIDList = null;
                if (row.GetMember("SAPID", out object objSapID))
                    sapIDList = objSapID.ToString().Split(new string[] { Common.Constants.cString_AggDelimeter.ToString() }, StringSplitOptions.None);

                //все построенные списки должны иметь одинаковое количество элементов - иначе нельзя будет построить соответствия значений извлечённых из списков
                if ((devIDList.Count() != assemblyProtocolIDList.Count()) || (devIDList.Count() != sapIDList.Count()))
                    throw new Exception(string.Format("ReportByDevices. CheckAssemblyProtocolIDAndSapID. Values ​​are not equal. devIDList.Count()={0}, assemblyProtocolIDList.Count()={1}, sapIDList.Count()={2}.", devIDList.Count().ToString(), assemblyProtocolIDList.Count().ToString(), sapIDList.Count().ToString()));

                //значения всех построенных списков упорядочены по DEV_ID и их количество одинаково - можно строить соответствия значений
                for (int i = 0; i < devIDList.Count(); i++)
                {
                    //считываем значение идентификатора измерения
                    //значения Null не может быть в принципе, может быть только число 
                    int devID = int.Parse(devIDList.ElementAt(i));

                    //считываем значение идентификатора протокола испытаний
                    //если его текущее значение не "EMPTY" - то оно может быть только числом
                    string sAssemblyProtocolID = assemblyProtocolIDList.ElementAt(i);
                    int assemblyProtocolID = (sAssemblyProtocolID == "EMPTY") ? -1 : int.Parse(sAssemblyProtocolID);

                    //считываем значение статуса в протоколе сборки
                    //значения Null быть не может, может быть либо "0", либо "1"
                    //microsoft не сделал возможным bool.Parse("1") и bool.Parse("0") - для них это не логический тип. поэтому:
                    bool sapID = (sapIDList.ElementAt(i) == "1");

                    if (((assemblyProtocolID == -1) && sapID) || ((assemblyProtocolID != -1) && !sapID))
                        throw new Exception(string.Format("ReportByDevices. CheckAssemblyProtocolIDAndSapID. assemblyProtocolID={0}, sapID={1}.", assemblyProtocolID, sapID));

                    //при сохранении используем идентификатор протокола сборки по которому строится данный отчёт
                    if ((assemblyProtocolID == -1) && !sapID)
                        DbRoutines.UpdateDeviceState(devID, int.Parse(this[0].AssemblyProtocolID), true);
                }
            }

            return null;
        }

        public string ChecksDataForAssemblyReport(DynamicObj row)
        {
            //реализация проверок данных принятого row для формирования отчёта по протоколу сборки в Excel
            //возвращает:
            // null - принятый row прошёл все проверки, ошибок в данных нет;
            // сообщение об ошибке, найденной в проверяемых данных

            //в записи с индексом 0 имеем выбранные пользователем значения параметров из шапки протокола сборки
            //проверяем, что данные из принятого на вход row соответствуют установленному пользователем значению dUdt
            ReportRecord zeroRecord = this[0];
            string dUdtUserValue = zeroRecord.DUdt;

            //dUdtUserValue может иметь либо значение null, либо int значение
            //если dUdtUserValue=null - пользователю всё равно какое значение dUdt в табличных данных - проверка значений dUdt успешно закончена
            if (dUdtUserValue == null)
                return null;

            if (int.TryParse(dUdtUserValue, out int idUdtUserValue))
            {
                List<string> memberNames = row.GetDynamicMemberNames().ToList();

                int conditionsInDataSourceFirstIndex = Routines.ConditionsFirstIndex(row);
                int parametersInDataSourceFirstIndex = Routines.ParametersFirstIndex(row);

                if ((conditionsInDataSourceFirstIndex != -1) || (parametersInDataSourceFirstIndex != -1))
                {
                    int startIndex = (conditionsInDataSourceFirstIndex == -1) ? parametersInDataSourceFirstIndex : conditionsInDataSourceFirstIndex;

                    const string dUdtName = "dvdt_voltagerate";
                    string name = memberNames.FirstOrDefault(n => (memberNames.IndexOf(n) >= startIndex) && (n.IndexOf(SCME.Common.Constants.FromXMLNameSeparator) != -1) && n.EndsWith(dUdtName));

                    if (name != null)
                    {
                        if (row.GetMember(name, out object value))
                        {
                            //запоминаем единицу измерения dUdt
                            zeroRecord.DUdtUnitMeasure = Dictionaries.ConditionUnitMeasure(dUdtName);

                            //в проверяемых данных значение dUdt может начинаться символом V
                            //проверяем это и избавляемся от начального символа V если он присутствует
                            int iValue = Routines.EndingNumberFromValue(value.ToString());

                            if (iValue >= idUdtUserValue)
                                return null;
                        }
                    }
                }
            }

            //раз мы здесь - значит проверка не пройдена, есть ошибки
            return string.Format(Properties.Resources.ReportBuildingError, "'dUdt'");
        }

        public void CalculatesForAssemblyReport(DynamicObj row)
        {
            //вычисляем min и max значения для нужных нам параметров
            //выполняем сравнение значений параметров с сохранением в записи с нулемым индексом
            ReportRecord zeroRecord = this[0];

            List<string> memberNames = row.GetDynamicMemberNames().ToList();

            int conditionsInDataSourceFirstIndex = Routines.ConditionsFirstIndex(row);
            int parametersInDataSourceFirstIndex = Routines.ParametersFirstIndex(row);

            if ((conditionsInDataSourceFirstIndex != -1) || (parametersInDataSourceFirstIndex != -1))
            {
                int startIndex = (conditionsInDataSourceFirstIndex == -1) ? parametersInDataSourceFirstIndex : conditionsInDataSourceFirstIndex;

                //вычисляем min и max по Igt
                string name = memberNames.FirstOrDefault(n => (memberNames.IndexOf(n) >= startIndex) && (n.IndexOf(SCME.Common.Constants.FromXMLNameSeparator) != -1) && n.EndsWith("igt"));

                if (name != null)
                {
                    if (row.GetMember(name, out object valueIgt))
                    {
                        if (Common.Routines.TryStringToDouble(valueIgt?.ToString(), out double igt))
                        {
                            if ((zeroRecord.IgtMin == 0) || (igt < zeroRecord.IgtMin))
                                zeroRecord.IgtMin = igt;

                            if (igt > zeroRecord.IgtMax)
                                zeroRecord.IgtMax = igt;

                            if (zeroRecord.IgtUnitMeasure == null)
                            {
                                name = Routines.NameOfUnitMeasure(name);

                                if (row.GetMember(name, out object unitMeasure))
                                    zeroRecord.IgtUnitMeasure = unitMeasure.ToString();
                            }
                        }
                    }
                }

                //вычисляем min и max по Ugt
                name = memberNames.FirstOrDefault(n => (memberNames.IndexOf(n) >= startIndex) && (n.IndexOf(SCME.Common.Constants.FromXMLNameSeparator) != -1) && n.EndsWith("vgt"));

                if (name != null)
                {
                    if (row.GetMember(name, out object valueUgt))
                    {
                        if (Common.Routines.TryStringToDouble(valueUgt?.ToString(), out double ugt))
                        {
                            if ((zeroRecord.UgtMin == 0) || (ugt < zeroRecord.UgtMin))
                                zeroRecord.UgtMin = ugt;

                            if (ugt > zeroRecord.UgtMax)
                                zeroRecord.UgtMax = ugt;

                            if (zeroRecord.UgtUnitMeasure == null)
                            {
                                name = Routines.NameOfUnitMeasure(name);

                                if (row.GetMember(name, out object unitMeasure))
                                    zeroRecord.UgtUnitMeasure = unitMeasure.ToString();
                            }
                        }
                    }
                }
            }
        }

        public string Load(List<DynamicObj> source)
        {
            //переписывам данные из принятого source в себя
            //если хотя бы для одного row из принятого source реализация this.ChecksDataForAssemblyReport вернула false - прекращаем исполнение данной реализации и возвращаем строку с ошибкой
            string result = null;

            foreach (DynamicObj row in source)
            {
                //проверяем данные
                result = this.CheckAssemblyProtocolIDAndSapID(row);

                //если процедура проверки обнаружила не соответствия - прекращаем заливку данных и возвращаем описание ошибки
                if (result != null)
                    return result;

                //проверяем данные
                result = this.ChecksDataForAssemblyReport(row);

                //если процедура проверки обнаружила не соответствия - прекращаем заливку данных и возвращаем описание ошибки
                if (result != null)
                    return result;

                //вычисляем min и max по нужным нам параметрам
                this.CalculatesForAssemblyReport(row);

                ReportRecord reportRecord = new ReportRecord(this, row);
                this.Add(reportRecord);
            }

            //все загруженные в this записи успешно прошли проверку
            return result;
        }

        public string Fill(int cacheSize)
        {
            List<DynamicObj> cacheData = new List<DynamicObj>();
            Routines.GetCacheData(cacheData, cacheSize);

            return this.Load(cacheData);
        }

        public string LoadFromDataBase(int assemblyProtocolID, out int recordCount)
        {
            //формирование отчёта по протоколу сборки
            //чтение данных из базы данных без использования кеша - пользователь формирует отчёт по сохранённым данным без возможности их изменения
            List<DynamicObj> data = new List<DynamicObj>();
            Routines.GetAssemblyProtocolData(assemblyProtocolID, data);

            string result = this.Load(data);
            recordCount = data.Count;

            return result;
        }

        public void QtyReleasedByGroupNameToExcel(SpreadsheetDocument spreadsheetDocument, ref uint rowNum, string groupName, int? qtyReleased)
        {
            this[0].QtyReleasedByGroupNameToExcel(spreadsheetDocument, ref rowNum, groupName, qtyReleased);
        }

        public void QtyOKFaultToExcel(SpreadsheetDocument spreadsheetDocument, uint rowNum, int totalCount, int statusUnknownCount, int statusFaultCount, int statusOKCount)
        {
            this[0].QtyOKFaultToExcel(spreadsheetDocument, rowNum, totalCount, statusUnknownCount, statusFaultCount, statusOKCount);
        }

        public void SetColumnWidth(SpreadsheetDocument spreadsheetDocument)
        {
            if (spreadsheetDocument != null)
            {
                Columns columns = OpenXmlRoutines.Columns(spreadsheetDocument);

                if (columns.ChildElements.Count() != 0)
                    columns.RemoveAllChildren();

                Dictionary<uint, double> maxWidthColumnsInPixel = OpenXmlRoutines.MaxWidthColumnsInPixel(spreadsheetDocument, 20);

                //формируем описание столбцов в columns - устанавливаем ширину столбца
                foreach (KeyValuePair<uint, double> entry in maxWidthColumnsInPixel)
                {
                    //устанавливаем значение ширины столбцов по хранящемуся в maxWidthColumnsInPixel значению
                    //maxWidthColumnsInPixel хранит значения которые перед применением необходимо пересчитать
                    columns.Append(new Column() { Min = DocumentFormat.OpenXml.UInt32Value.FromUInt32(entry.Key), Max = DocumentFormat.OpenXml.UInt32Value.FromUInt32(entry.Key), Width = OpenXmlRoutines.WidthToOpenXml(entry.Value), CustomWidth = DocumentFormat.OpenXml.BooleanValue.FromBoolean(true), BestFit = DocumentFormat.OpenXml.BooleanValue.FromBoolean(true) });
                }
            }
        }

        public void PageSetUp(SpreadsheetDocument spreadsheetDocument)
        {
            if (spreadsheetDocument != null)
            {
                WorksheetPart worksheetPart = spreadsheetDocument.WorkbookPart.GetPartsOfType<WorksheetPart>().First();

                if (worksheetPart != null)
                {
                    DocumentFormat.OpenXml.Spreadsheet.Worksheet worksheet = worksheetPart.Worksheet;

                    if (worksheet != null)
                    {
                        SheetProperties sheetProperties = new SheetProperties(new PageSetupProperties());
                        sheetProperties.PageSetupProperties.FitToPage = DocumentFormat.OpenXml.BooleanValue.FromBoolean(true);
                        worksheet.SheetProperties = sheetProperties;

                        //PageMargins должен быть добавлен после SheetData, но перед PageSetup
                        PageMargins pageMargins = new PageMargins()
                        {
                            Left = DocumentFormat.OpenXml.DoubleValue.FromDouble(0.195),
                            Right = DocumentFormat.OpenXml.DoubleValue.FromDouble(0.195),
                            Top = DocumentFormat.OpenXml.DoubleValue.FromDouble(0.195),
                            Bottom = DocumentFormat.OpenXml.DoubleValue.FromDouble(0.5),
                            Header = DocumentFormat.OpenXml.DoubleValue.FromDouble(0),
                            Footer = DocumentFormat.OpenXml.DoubleValue.FromDouble(0.195)
                        };
                        worksheet.AppendChild(pageMargins);

                        DocumentFormat.OpenXml.Spreadsheet.PageSetup pageSetup = new DocumentFormat.OpenXml.Spreadsheet.PageSetup()
                        {
                            Orientation = OrientationValues.Landscape,
                            PaperSize = DocumentFormat.OpenXml.UInt32Value.FromUInt32(9), //A4
                            FitToHeight = DocumentFormat.OpenXml.UInt32Value.FromUInt32(1),
                            FitToWidth = DocumentFormat.OpenXml.UInt32Value.FromUInt32(1),
                            Scale = DocumentFormat.OpenXml.UInt32Value.FromUInt32(1),
                        };
                        worksheet.AppendChild(pageSetup);
                    }
                }
            }
        }

        public void ListOfCalculatorsMinMaxToExcel(SpreadsheetDocument spreadsheetDocument, ref uint rowNum, ListOfCalculatorsMinMax listOfCalculatorsMinMax)
        {
            this[0].ListOfCalculatorsMinMaxToExcel(spreadsheetDocument, ref rowNum, listOfCalculatorsMinMax);
        }

        public void ReportToExcel()
        {
            //удаляем все старые отчёты, чтобы не захламлять директорию
            Routines.ClearOldReports(Routines.EnvironmentVariableTempValue(), Routines.PartOfCasualReportFileName);

            string fileFullAddress = GetReportFileFullAddress(string.Concat(Routines.PartOfCasualReportFileName, " ", DateTime.Now.ToString("dd.MM.yyyy")));

            SpreadsheetDocument spreadsheetDocument = OpenXmlRoutines.Create(fileFullAddress);

            try
            {
                DocumentFormat.OpenXml.Spreadsheet.Worksheet worksheet = OpenXmlRoutines.CreateSheet(spreadsheetDocument, "Протокол испытаний");

                //здесь будем хранить флаг о неизменности ПЗ во всём выведенном отчёте
                bool groupNameChanged = false;

                //здесь будем хранить предыдущий просмотренный в цикле GroupName
                string lastGroupName = null;

                string lastUsedColumnsSignature = string.Empty;

                //счётчики успешного и не успешного прохождения тестов
                int statusOKCount = 0;
                int statusFaultCount = 0;
                int statusUnknownCount = 0;
                int totalCount = 0;

                uint lastUsedColumn = 0;

                uint rowNum = 1;
                uint rowNumBeg;
                uint columnEnd = 0;

                //храним здесь сколько шапок было выведено
                int needHeaderCount = 0;

                ListOfCalculatorsMinMax listOfCalculatorsMinMax = new ListOfCalculatorsMinMax();

                foreach (ReportRecord p in this)
                {
                    string currentGroupName = p.GroupName;

                    if ((!groupNameChanged) && (lastGroupName != null))
                        groupNameChanged = (currentGroupName != lastGroupName);

                    uint column = 1;

                    bool needHeader = (lastUsedColumnsSignature == string.Empty) || (lastUsedColumnsSignature != p.ColumnsSignature);

                    lastGroupName = currentGroupName;

                    //выводим шапку если имеет место смена списка выведенных столбцов
                    uint headerRowNum = rowNum + 2;
                    rowNumBeg = rowNum;

                    if (needHeader)
                    {
                        columnEnd = column + 2;

                        //выводим самую верхнюю часть шапки
                        p.TopHeaderToExcel(spreadsheetDocument, ref rowNum);

                        //выводим шапку столбцов условий и параметров
                        p.HeaderCPToExcel(spreadsheetDocument, ref rowNum, ref columnEnd);

                        //выводим шапку идентификационных данных
                        p.HeaderToExcel(spreadsheetDocument, ref rowNum, column + 1);

                        //выводим шапку статуса
                        p.StatusHeaderToExcel(spreadsheetDocument, rowNum, columnEnd);

                        needHeaderCount++;
                        rowNum++;
                    }

                    //выводим идентификационные данные
                    p.IdentityToExcel(spreadsheetDocument, rowNum, ref column);

                    //выводим тело
                    p.BodyToExcel(spreadsheetDocument, listOfCalculatorsMinMax, rowNum, ref column);

                    //выводим статус
                    bool? isStatusOK = null;
                    lastUsedColumn = p.StatusToExcel(spreadsheetDocument, rowNum, columnEnd, ref isStatusOK);

                    //формируем значения счётчиков неопределённого/успешного/не успешного прохождения тестов
                    if (isStatusOK == null)
                        statusUnknownCount++;
                    else
                    {
                        switch (isStatusOK)
                        {
                            case false:
                                statusFaultCount++;
                                break;

                            default:
                                statusOKCount++;
                                break;
                        }
                    }

                    //считаем сколько всего изделий просмотрено в цикле
                    totalCount++;

                    //запоминаем набор столбцов, которые мы вывели
                    lastUsedColumnsSignature = p.ColumnsSignature;

                    rowNum++;
                }

                //если на протяжении всего цикла не зафиксировано изменение ПЗ - выводим кол-во запущенных ТМЦ по ПЗ. если же изменение ПЗ зафиксировано - в отчёте есть данные по разным ПЗ и запущенное кол-во не имеет смысла
                if (!groupNameChanged)
                {
                    int? qtyReleased = DbRoutines.QtyReleasedByGroupName(lastGroupName);

                    //выводим значения min/max для определённых в listOfCalculatorsMinMax параметров
                    if (needHeaderCount == 1)
                        this.ListOfCalculatorsMinMaxToExcel(spreadsheetDocument, ref rowNum, listOfCalculatorsMinMax);

                    //выводим количество ТМЦ, запущенных по ПЗ
                    this.QtyReleasedByGroupNameToExcel(spreadsheetDocument, ref rowNum, lastGroupName, qtyReleased);

                    //выводим кол-во годных/не годных
                    this.QtyOKFaultToExcel(spreadsheetDocument, rowNum, totalCount, statusUnknownCount, statusFaultCount, statusOKCount);
                }

                this.SetColumnWidth(spreadsheetDocument);

                //настраиваем вид печатного отчёта
                this.PageSetUp(spreadsheetDocument);

                //создаём нижний колонтитул
                //колонтитулы должны быть созданы строго после настройки вида печатного отчёта
                OpenXmlRoutines.CreateHeaderFooter(worksheet, null, "&R&\"Arial Narrow,Regular\"Лист &P, листов &N, напечатан &D");

                spreadsheetDocument.WorkbookPart.Workbook.Save();
            }
            finally
            {
                //освобождаем ресурсы, занятые spreadsheetDocument
                spreadsheetDocument.Dispose();
            }

            //открываем созданный файл тем редактором, который ассоциирован с расширением созданного файла
            System.Diagnostics.Process.Start(fileFullAddress);
        }

        /*
        public void Print()
        {
            //печать уже сформированного отчёта
            this.sheet?.PrintOut(Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
        }
        */

        //нормы на параметр для протокола сборки
        public class ParamReference
        {
            public string Name { get; set; }
            public object MinValue { get; set; }
            public object MaxValue { get; set; }

            public string NrmDescr()
            {
                //формирует строковое описание хранящихся в себе норм
                return Routines.NrmDescr(this.MinValue, this.MaxValue);
            }
        }

        private void LoadNorms(int itav, int deviceTypeID, string constructive, string modification, List<ParamReference> norms)
        {
            //принимает на вход norms и формирует в нём список имён параметров с описанием границ их значений
            //границы в таблице DEVICEREFERENCES описаны так: либо есть верхняя граница значений параметра, либо нижняя, есть возможность хранения в norms и нижней и верхней границы
            //считываем описание норм из справочника с нормами на изделия DeviceReferences
            if (norms != null)
            {
                if (DbRoutines.DeviceReferences(itav, deviceTypeID, constructive, modification, out int? idrmMax, out decimal? utmMax, out int? qrrMax, out int? igtMax, out decimal? ugtMax, out int? tjMax, out int? prsmMin, out string caseType, out decimal? utmCorrection) != -1)
                {
                    norms.Clear();
                    ParamReference paramReference;

                    if (idrmMax != null)
                    {
                        //то, что справедливо для IDRM, так же справедливо и для IRRM
                        paramReference = new ParamReference { Name = "IDRM", MinValue = null, MaxValue = idrmMax };
                        norms.Add(paramReference);

                        paramReference = new ParamReference { Name = "IRRM", MinValue = null, MaxValue = idrmMax };
                        norms.Add(paramReference);
                    }

                    if (utmMax != null)
                    {
                        paramReference = new ParamReference { Name = "UTM", MinValue = null, MaxValue = ((decimal)utmMax).ToString("0.00") };
                        norms.Add(paramReference);
                    }

                    if (qrrMax != null)
                    {
                        paramReference = new ParamReference { Name = Common.Constants.Qrr, MinValue = null, MaxValue = qrrMax };
                        norms.Add(paramReference);
                    }

                    if (igtMax != null)
                    {
                        paramReference = new ParamReference { Name = "IGT", MinValue = null, MaxValue = igtMax };
                        norms.Add(paramReference);
                    }

                    if (ugtMax != null)
                    {
                        paramReference = new ParamReference { Name = "UGT", MinValue = null, MaxValue = ((decimal)ugtMax).ToString("0.00") };
                        norms.Add(paramReference);
                    }

                    if (tjMax != null)
                    {
                        paramReference = new ParamReference { Name = "TjMax", MinValue = null, MaxValue = tjMax };
                        norms.Add(paramReference);
                    }

                    if (prsmMin != null)
                    {
                        paramReference = new ParamReference { Name = "PRSM", MinValue = prsmMin, MaxValue = null };
                        norms.Add(paramReference);
                    }
                }
            }
        }

        public class NameDescr
        {
            public NameDescr(string name, Common.Routines.XMLValues subject, string test, TemperatureCondition temperatureCondition, string temperatureValue, string unitMeasure, string nrm)
            {
                this.Name = name;
                this.Subject = subject;
                this.Test = test;
                this.TemperatureCondition = temperatureCondition;
                this.TemperatureValue = temperatureValue;
                this.UnitMeasure = unitMeasure;
                this.Nrm = nrm;
            }

            public string Name { get; }
            public Common.Routines.XMLValues Subject { get; }
            public string Test { get; }
            public TemperatureCondition TemperatureCondition { get; }
            public string TemperatureValue { get; }
            public string UnitMeasure { get; }
            public string Nrm { get; }
        }

        private List<NameDescr> AllNames(int itav, int deviceTypeID, string constructive, string modification, List<ParamReference> norms)
        {
            //вычисляет множество уникальных имён параметров/условий по всему имеющемуся списку записей в this, устанавливает порядок их следования как хотят технологи
            //удаляет из сформированного списка те параметры, которые технологи не хотят видеть в отчёте
            //каждая запись несёт в себе свой собственный набор имён параметров/условий
            //параметры, созданные вручную технологи хотят выдеть в конце итогового списка, порядок их следования произвольный
            List<NameDescr> result = new List<NameDescr>();

            if (norms != null)
            {
                List<NameDescr> pc = new List<NameDescr>();

                //здесь будем держать параметры созданные пользователем вручную
                List<NameDescr> pcManually = new List<NameDescr>();

                //считываем список желаемых технологами параметров стоящих в нужном порядке и переводим его в верхний регистр
                //имена: dvdt_voltagerate, igt, ugt исключаем из результирующего списка с целью экономии ширины отчёта, под сформированными табличными данными будем выводить значение dvdt_voltagerate, а для параметров igt, ugt - минимум и максимум
                //при этом на момент работы данной реализации уже проверено, что все значения dvdt_voltagerate формируемого отчёта удовлетворяют выбранному пользователем значению в шапке протокола сборки
                List<string> referenceNames = Constants.OrderedColumnNamesInReport.Where(n => n != "dvdt_voltagerate" && n != "igt" && n != "vgt" && n != "rg").Select(n => n.ToUpper()).ToList();

                //считываем нормы для выбранных пользователем среднего тока, типа изделия, конструктива и модификации
                this.LoadNorms(itav, deviceTypeID, constructive, modification, norms);

                //перебираем все записи и собираем в result множество уникальных имён параметров/условий
                //в записи с индексом 0 хрянятся реквизиты самого протокола сборки, параметров и условий в ней нет
                for (int i = 1; i < this.Count; i++)
                {
                    ReportRecord r = this[i];

                    int conditionsInDataSourceFirstIndex = r.ConditionsFirstIndex();
                    int parametersInDataSourceFirstIndex = r.ParametersFirstIndex();

                    if ((conditionsInDataSourceFirstIndex != -1) || (parametersInDataSourceFirstIndex != -1))
                    {
                        int startIndex = (conditionsInDataSourceFirstIndex == -1) ? parametersInDataSourceFirstIndex : conditionsInDataSourceFirstIndex;
                        List<string> memberNames = r.Row.GetDynamicMemberNames().ToList();
                        IEnumerable<string> listOfColumnNames = memberNames.Where(n => (memberNames.IndexOf(n) >= startIndex) && (n.IndexOf(SCME.Common.Constants.FromXMLNameSeparator) != -1) && !n.EndsWith(Constants.HiddenMarker));

                        foreach (string name in listOfColumnNames)
                        {
                            string trueName = Routines.ParseColumnName(name, out string test, out string sTemperatureCondition);

                            if (Enum.TryParse(sTemperatureCondition, out TemperatureCondition temperatureCondition))
                            {
                                //считываем значение температуры при которой выполнялось измерение значения параметра name
                                //считанное значение будет вида 25°C
                                string temperatureValue = r.TemperatureByColumnName(name);

                                //определяем с чем мы имеем дело
                                int columnIndex = memberNames.IndexOf(name);
                                Common.Routines.XMLValues subject = (columnIndex < parametersInDataSourceFirstIndex) ? Common.Routines.XMLValues.Conditions : (test == DbRoutines.cManually.ToUpper()) ? Common.Routines.XMLValues.ManuallyParameters : Common.Routines.XMLValues.Parameters;

                                //технологи хотят чтобы параметры созданные вручную выводились в конце списка всех параметров - будем держать их в pcManually чтобы не путать с обычными параметрами/условиями
                                List<NameDescr> storage = (subject == Common.Routines.XMLValues.ManuallyParameters) ? pcManually : pc; //(test == DbRoutines.cManually.ToUpper()) ? pcManually : pc;

                                string nrm = null;
                                switch (subject)
                                {
                                    //случай параметров которые созданы пользователем вручную
                                    case Common.Routines.XMLValues.ManuallyParameters:
                                        nrm = r.NrmByColumnName(name);
                                        break;

                                    //для всех измеряемых параметров нормы считываются исключительно из справочника норм, которые технологи заполняют в таблице DeviceReferences,
                                    //нормы которые указаны в профиле в отчёте по протоколу сборки не используются
                                    case Common.Routines.XMLValues.Parameters:
                                        ParamReference paramReference = norms.Where(item => item.Name == Dictionaries.ParameterName(trueName)).FirstOrDefault();
                                        nrm = paramReference?.NrmDescr();
                                        break;
                                }

                                //проверяем в storage наличие текущего name с тестом test, температурой temperatureValue и нормами nrm
                                //нормы в этом списке нужны для того, чтобы создать новый столбец в отчёте если описание норм для параметра trueName теста test с температурным режимом temperatureValue будет иметь не одинаковые значения для всех записей отчёта
                                IEnumerable<NameDescr> founded = storage.Where(n => (n.Name == trueName) && (n.Test == test) && (n.TemperatureValue == temperatureValue) && (n.Nrm == nrm));

                                //рассматриваем два случая:
                                // - параметр создан пользователем вручную. если в результирующем наборе нет текущего параметра с именем name - добавляем его
                                // - параметр не является пользовательским. если в результирующем наборе нет текущего параметра с именем name, с тестом test и температурой temperatureCondition и данный параметр является желаемым параметром - добавляем его
                                bool need = (founded.Count() == 0) && ((test == DbRoutines.cManually.ToUpper()) || (test != DbRoutines.cManually.ToUpper()) && (referenceNames.Where(n => n == trueName).Count() != 0));

                                if (need)
                                {
                                    //считываем единицу измерения
                                    string nameOfUnitMeasure = Routines.NameOfUnitMeasure(name);
                                    string unitMeasure = null;

                                    if (r.Row.GetMember(nameOfUnitMeasure, out object value))
                                        unitMeasure = value?.ToString();

                                    NameDescr nameDescr = new NameDescr(trueName, subject, test, temperatureCondition, temperatureValue, unitMeasure, nrm);
                                    storage.Add(nameDescr);
                                }
                            }
                        }
                    }
                }

                //множество параметров/условий уникальных по тесту, тепловому режиму и имени сформировано
                //в нём присутствуют только те параметры/условия, которые есть в списке Constants.OrderedColumnNamesInReport
                //формируем выходное множество в соответствии с желаемым технологами порядком следования столбцов в отчёте
                foreach (string name in referenceNames)
                {
                    IEnumerable<NameDescr> founded = pc.Where(n => n.Name == name);

                    foreach (NameDescr param in founded)
                        result.Add(param);
                }

                //дописываем в конец сформированного списка параметры созданные вручную
                foreach (NameDescr param in pcManually)
                    result.Add(param);
            }

            return result;
        }

        private string GenerateReportFileFullAddress(string reportDescr, uint? additionFileName)
        {
            string sAdditionFileName = (additionFileName == null) ? string.Empty : string.Concat("_", additionFileName.ToString());

            return string.Concat(Routines.EnvironmentVariableTempValue(), reportDescr, sAdditionFileName, ".xlsx");
        }

        private string GetReportFileFullAddress(string reportDescr)
        {
            //формирует полное имя файла отчёта
            //файл со сгенерированным именем гарантированно не занят в системе

            string result = GenerateReportFileFullAddress(reportDescr, null);

            if (System.IO.File.Exists(result) && Routines.IsFileLocked(result))
            {
                uint additionFileName = 0;

                while (Routines.IsFileLocked(result))
                {
                    result = GenerateReportFileFullAddress(reportDescr, additionFileName);

                    if (!System.IO.File.Exists(result))
                        break;

                    additionFileName++;
                }
            }

            return result;
        }

        public void AssemblyReportToExcel(int itav, int deviceTypeID, string constructive, string modification)
        {
            //построение отчёта протокола сборки
            //входные параметры:
            // itav - средний ток;
            // deviceTypeID - идентификатор типа изделия;
            // constructive - конструктив;
            // modification - модификация

            //считываем обозначение протокола сборки - его значение одинаково для любой записи, кроме нулевой записи специально созданной для передачи и хранения выбранных пользователем значений реквизитов, но обозначения протокола сборки в ней нет
            string assemblyProtocolDescr = this[1].AssemblyProtocolDescr;

            //удаляем все старые отчёты по протоколу сборки, чтобы не захламлять директорию
            Routines.ClearOldReports(Routines.EnvironmentVariableTempValue(), Routines.PartOfAssemblyProtocolFileName);

            string fileFullAddress = GetReportFileFullAddress(string.Concat(Routines.PartOfAssemblyProtocolFileName, " ", assemblyProtocolDescr));

            SpreadsheetDocument spreadsheetDocument = OpenXmlRoutines.Create(fileFullAddress);

            try
            {
                OpenXmlRoutines.CreateSheet(spreadsheetDocument, "Протокол сборки");

                uint? firstUserColumn = null;
                uint? lastUserColumn = null;
                uint? lastColumn = null;

                //выводим шапку отчёта
                //она не зависит от данных, которые выводятся в цикле
                //в нулевой записи хранятся данные для всего протокола, справедливые для любой записи
                uint rowNum = 1;

                ReportRecord zeroRecord = this[0];
                zeroRecord.AssemblyTopHeaderToExcel(spreadsheetDocument, assemblyProtocolDescr, ref rowNum);

                //вычисляем список столбцов для данного отчёта
                List<ParamReference> norms = new List<ParamReference>();
                List<NameDescr> allNames = this.AllNames(itav, deviceTypeID, constructive, modification, norms);

                //начинаем именно с записи с индексом 1
                for (int i = 1; i < this.Count; i++)
                {
                    ReportRecord r = this[i];

                    //выводим имена столбцов табличных данных один раз на весь отчёт
                    if (i == 1)
                    {
                        r.TableColumnNamesToExcel(spreadsheetDocument, allNames, rowNum, out firstUserColumn, out lastUserColumn, out lastColumn);
                        rowNum = 9;
                    }

                    //выводим идентификационные данные в таблицу
                    r.TableIdentityDataToExcel(spreadsheetDocument, i, rowNum);

                    //выводим значения parameters/conditions  в таблицу
                    r.TableBodyToExcel(spreadsheetDocument, allNames, rowNum);

                    if (lastColumn != null)
                    {
                        if (firstUserColumn != null)
                            r.MakeEmptyCells(spreadsheetDocument, rowNum, (uint)firstUserColumn, (uint)lastColumn);

                        //выводим значения комментариев
                        r.DeviceCommentsToExcel(spreadsheetDocument, rowNum, (uint)lastColumn);
                    }

                    rowNum++;
                }

                rowNum--;

                //ширина, выставленная автоматически слишком мала для заполнения руками - выставляем свою ширину
                //пользователи данного отчёта пишут руками некоторые данные в распечатанном отчёте и высота ячеек, установленных автоматически по самой высокой ячейке в строке им мала - поэтому
                zeroRecord.SetColumnWidth(spreadsheetDocument, (uint)firstUserColumn, (uint)lastUserColumn, 20);

                //выводим данные из нулевой строки под выведенной таблицей
                rowNum += 2;
                zeroRecord.BottomDataToExcel(spreadsheetDocument, norms, rowNum, (uint)lastColumn);

                zeroRecord.PageSetup(spreadsheetDocument);

                spreadsheetDocument.WorkbookPart.Workbook.Save();
            }
            finally
            {
                //освобождаем ресурсы, занятые spreadsheetDocument
                spreadsheetDocument.Dispose();
            }

            //открываем созданный файл тем редактором, который ассоциирован с расширением созданного файла
            System.Diagnostics.Process.Start(fileFullAddress);
        }
    }



    /*
    public class ReportData
    {
        public DataTableParameters RTData { get; set; }
        public DataTableParameters TMData { get; set; }

        public string ColumnsSignature
        {
            get
            {
                string result = string.Empty;

                switch (this.RTData == null)
                {
                    case true:
                        result = this.TMData?.ColumnsSignature;
                        break;

                    default:
                        result = (this.TMData == null) ? this.RTData?.ColumnsSignature : this.RTData?.ColumnsSignature + this.TMData?.ColumnsSignature;
                        break;
                }

                return result;
            }
        }

        public string Status
        {
            get
            {
                //чтобы вернуть OK надо, чтобы все тесты завершились с результатом OK. если хотя-бы один тест закончился с результатом Fault - вернём Fault
                string result = "Fault";

                string goodStatus = "OK";
                result = RTData?.Status;

                if (result == goodStatus)
                    result = RTData?.Status;

                return result;
            }
        }

        public string Reason
        {
            get
            {
                string result = string.Empty;

                //не формируем описания абсолютно всех проблем ибо пользователь их все читать точно не будет - ограничиваемся первой найденной проблемой
                result = RTData?.Reason;

                if (result == string.Empty)
                    result = TMData?.Reason;

                return result;
            }
        }

        public string Code
        {
            get
            {
                return (RTData == null) ? TMData.Code : RTData.Code;
            }
        }

        public void TopHeaderToExcel(Excel.Application exelApp, Excel.Worksheet sheet, ref int rowNum)
        {
            switch (this.RTData == null)
            {
                case true:
                    //TMData точно не null
                    this.TMData.TopHeaderToExcel(exelApp, sheet, ref rowNum);
                    break;

                default:
                    this.RTData.TopHeaderToExcel(exelApp, sheet, ref rowNum);
                    break;
            }
        }

        public void ColumnsHeaderToExcel(Excel.Application exelApp, Excel.Worksheet sheet, ref int rowNum, ref int column)
        {
            switch (this.RTData == null)
            {
                case true:
                    //TMData точно не null
                    this.TMData.ColumnsHeaderToExcel(exelApp, sheet, ref rowNum, ref column);
                    break;

                default:
                    this.RTData.ColumnsHeaderToExcel(exelApp, sheet, ref rowNum, ref column);
                    break;
            }
        }

        public void HeaderToExcel(Excel.Application exelApp, Excel.Worksheet sheet, ref int rowNum, int column, ref int columnEnd)
        {
            switch (this.RTData == null)
            {
                case true:
                    //TMData точно не null
                    this.TMData.HeaderToExcel(exelApp, sheet, ref rowNum, ref column, ref columnEnd);
                    break;

                default:
                    this.RTData.HeaderToExcel(exelApp, sheet, ref rowNum, ref column, ref columnEnd);
                    break;
            }
        }

        public void PairColumnsHeaderToExcel(Excel.Application exelApp, Excel.Worksheet sheet, int rowNum, ref int column)
        {
            if ((this.RTData != null) && (this.TMData != null))
                this.TMData.ColumnsHeaderToExcel(exelApp, sheet, ref rowNum, ref column);
        }

        public void PairHeaderToExcel(Excel.Application exelApp, Excel.Worksheet sheet, int rowNum, ref int column)
        {
            if ((this.RTData != null) && (this.TMData != null))
                this.TMData.PairHeaderToExcell(exelApp, sheet, rowNum, ref column);
        }


        public void StatusHeaderToExcel(Excel.Application exelApp, Excel.Worksheet sheet, int rowNum, int column)
        {
            switch (this.RTData == null)
            {
                case true:
                    //TMData точно не null
                    this.TMData.StatusHeaderToExcel(exelApp, sheet, rowNum, column);
                    break;

                default:
                    this.RTData.StatusHeaderToExcel(exelApp, sheet, rowNum, column);
                    break;
            }
        }

        public int StatusToExcel(Excel.Application exelApp, Excel.Worksheet sheet, int rowNum, int column, ref bool? isStatusOK)
        {
            //статус вычисляется по холодному и горячему измерениям
            //если хотя-бы одно измерение отсутствует - возвращаем неопределённый (пустое значение) статус
            //значение статуса "Ok" выводим только если для холодного и горячего измерений имеем статусы "Ok". если хотя-бы один из статусов не "Ok" - выводим "Fault"
            string rtStatus = this.RTData?.Status.Trim();
            string tmStatus = this.TMData?.Status.Trim();

            string status = string.Empty;

            //только если оба измерения завершились успешно - возвратим статус "OK"
            if ((rtStatus == "OK") && (tmStatus == "OK"))
                status = "OK";
            else
            {
                //если хотя-бы одно измерение завершилось не успешно - возвратим статус "Fault"
                if ((rtStatus == "Fault") || (tmStatus == "Fault"))
                    status = "Fault";
                else status = string.Empty;
            }

            if (status == "OK")
            {
                isStatusOK = true;
            }
            else
            {
                if (status == "Fault")
                {
                    isStatusOK = false;
                }
                else
                    isStatusOK = null;
            }

            string rtCodeOfNonMatch = (this.RTData?.CodeOfNonMatch == null) ? string.Empty : this.RTData?.CodeOfNonMatch;
            string tmCodeOfNonMatch = (this.TMData?.CodeOfNonMatch == null) ? string.Empty : this.TMData?.CodeOfNonMatch;

            string codeOfNonMatch = rtCodeOfNonMatch;

            if (tmCodeOfNonMatch != string.Empty)
            {
                if (codeOfNonMatch != string.Empty)
                    codeOfNonMatch += ", ";

                codeOfNonMatch += tmCodeOfNonMatch;
            }

            int result;

            switch (this.RTData == null)
            {
                case true:
                    //TMData точно не null
                    result = this.TMData.StatusToExcel(exelApp, sheet, status, codeOfNonMatch, rowNum, column);
                    break;

                default:
                    result = this.RTData.StatusToExcel(exelApp, sheet, status, codeOfNonMatch, rowNum, column);
                    break;
            }

            return result;
        }

        public void CounterToExcel(Excel.Application exelApp, Excel.Worksheet sheet, int counter, int rowNum)
        {
            switch (this.RTData == null)
            {
                case true:
                    //TMData точно не null
                    this.TMData.CounterToExcel(exelApp, sheet, counter, rowNum);
                    break;

                default:
                    this.RTData.CounterToExcel(exelApp, sheet, counter, rowNum);
                    break;
            }
        }

        public void ListOfCalculatorsMinMaxToExcel(Excel.Application exelApp, Excel.Worksheet sheet, int rowNum, ListOfCalculatorsMinMax listOfCalculatorsMinMax)
        {
            switch (this.RTData == null)
            {
                case true:
                    //TMData точно не null
                    this.TMData.ListOfCalculatorsMinMaxToExcel(exelApp, sheet, rowNum, listOfCalculatorsMinMax);
                    break;

                default:
                    this.RTData.ListOfCalculatorsMinMaxToExcel(exelApp, sheet, rowNum, listOfCalculatorsMinMax);
                    break;
            }
        }

        public void IdentityToExcel(Excel.Application exelApp, Excel.Worksheet sheet, int counter, int rowNum, ref int column)
        {
            switch (this.RTData == null)
            {
                case true:
                    //TMData точно не null
                    this.TMData.IdentityToExcel(exelApp, sheet, counter, rowNum, ref column);
                    break;

                default:
                    this.RTData.IdentityToExcel(exelApp, sheet, counter, rowNum, ref column);
                    break;
            }
        }

        public void EndIdentityToExcel(Excel.Application exelApp, Excel.Worksheet sheet, int rowNum, ref int column)
        {
            switch (this.RTData == null)
            {
                case true:
                    //TMData точно не null
                    this.TMData.EndIdentityToExcel(exelApp, sheet, rowNum, ref column);
                    break;

                default:
                    this.RTData.EndIdentityToExcel(exelApp, sheet, rowNum, ref column);
                    break;
            }

        }

        public void BodyToExcel(Excel.Application exelApp, Excel.Worksheet sheet, ListOfCalculatorsMinMax listOfCalculatorsMinMax, int rowNum, ref int column)
        {
            switch (this.RTData == null)
            {
                case true:
                    //TMData точно не null
                    this.TMData.BodyToExcel(exelApp, sheet, listOfCalculatorsMinMax, rowNum, ref column);
                    break;

                default:
                    this.RTData.BodyToExcel(exelApp, sheet, listOfCalculatorsMinMax, rowNum, ref column);
                    break;
            }
        }

        public void PairBodyToExcel(Excel.Application exelApp, Excel.Worksheet sheet, ListOfCalculatorsMinMax listOfCalculatorsMinMax, int rowNum, ref int column)
        {
            if ((this.RTData != null) && (this.TMData != null))
                this.TMData.BodyToExcel(exelApp, sheet, listOfCalculatorsMinMax, rowNum, ref column);
        }

        public void PairIdentityToExcel(Excel.Application exelApp, Excel.Worksheet sheet, int rowNum, int column)
        {
            if ((this.RTData != null) && (this.TMData != null))
                this.TMData.PairIdentityToExcel(exelApp, sheet, rowNum, column);
        }

        public void SetBorders(Excel.Application exelApp, Excel.Worksheet sheet, int rowNumBeg, int rowNumEnd, int columnEnd)
        {
            DataTableParameters dtp = (this.RTData == null) ? this.TMData : this.RTData;
            dtp?.SetBorders(exelApp, sheet, rowNumBeg, rowNumEnd, columnEnd);
        }

        public int? QtyReleasedByGroupName(string lastGroupName, SqlConnection connection)
        {
            int? result;

            switch (this.RTData == null)
            {
                case true:
                    //TMData точно не null
                    result = this.TMData.QtyReleasedByGroupName(lastGroupName, connection);
                    break;

                default:
                    result = this.RTData.QtyReleasedByGroupName(lastGroupName, connection);
                    break;
            }

            return result;
        }

        public void QtyReleasedByGroupNameToExcel(Excel.Application exelApp, Excel.Worksheet sheet, ref int rowNum, string groupName, int? qtyReleased)
        {
            //выводим кол-во запущенных по ЗП изделий
            switch (this.RTData == null)
            {
                case true:
                    //TMData точно не null
                    this.TMData.QtyReleasedByGroupNameToExcel(exelApp, sheet, ref rowNum, groupName, qtyReleased);
                    break;

                default:
                    this.RTData.QtyReleasedByGroupNameToExcel(exelApp, sheet, ref rowNum, groupName, qtyReleased);
                    break;
            }
        }

        public void QtyOKFaultToExcel(Excel.Application exelApp, Excel.Worksheet sheet, int rowNum, int totalCount, int statusUnknownCount, int statusFaultCount, int statusOKCount)
        {
            //выводим кол-во годных/не годных
            switch (this.RTData == null)
            {
                case true:
                    //TMData точно не null
                    this.TMData.QtyOKFaultToExcel(exelApp, sheet, rowNum, totalCount, statusUnknownCount, statusFaultCount, statusOKCount);
                    break;

                default:
                    this.RTData.QtyOKFaultToExcel(exelApp, sheet, rowNum, totalCount, statusUnknownCount, statusFaultCount, statusOKCount);
                    break;
            }
        }


        public void PaintRT(Excel.Application exelApp, Excel.Worksheet sheet, int rowNumBeg, int rowNumEnd, int colunmBeg, int columnEnd)
        {
            //красим холодное при наличии горячего
            if (this.TMData != null)
                this.RTData?.Paint(exelApp, sheet, rowNumBeg, rowNumEnd, colunmBeg, columnEnd, TemperatureCondition.RT);
        }

        public void PaintSingleRT(Excel.Application exelApp, Excel.Worksheet sheet, int rowNumBeg, int rowNumEnd, int colunmBeg, int columnEnd)
        {
            //красим холодное при отсутствии горячего
            if (this.TMData == null)
                this.RTData?.Paint(exelApp, sheet, rowNumBeg, rowNumEnd, colunmBeg, columnEnd, TemperatureCondition.RT);
        }

        public void PaintTM(Excel.Application exelApp, Excel.Worksheet sheet, int rowNumBeg, int rowNumEnd, int colunmBeg, int columnEnd)
        {
            //красим горячее в при наличии холодного
            if (this.RTData != null)
                this.TMData?.Paint(exelApp, sheet, rowNumBeg, rowNumEnd, colunmBeg, columnEnd, TemperatureCondition.TM);
        }

        public void PaintSingleTM(Excel.Application exelApp, Excel.Worksheet sheet, int rowNumBeg, int rowNumEnd, int colunmBeg, int columnEnd)
        {
            //красим горячее в при отсутствии холодного
            if (this.RTData == null)
                this.TMData?.Paint(exelApp, sheet, rowNumBeg, rowNumEnd, colunmBeg, columnEnd, TemperatureCondition.TM);
        }
    }
    */


    /*
    public class ReportByDevices : List<ReportData>
    {
        Excel.Application exelApp = null;
        Excel.Worksheet sheet = null;

        private string LastUsedColumnsSignature { get; set; }

        public ReportByDevices() : base()
        {
        }

        public ReportByDevices(List<ReportData> source) : base(source)
        {
        }

        public ReportData NewReportData()
        {
            ReportData result = new ReportData();
            this.Add(result);

            return result;
        }

        public void ToExcel(SqlConnection connection, bool visibleAfterBuilding)
        {
            this.exelApp = new Microsoft.Office.Interop.Excel.Application();
            this.exelApp.Visible = false;

            try
            {
                this.exelApp.SheetsInNewWorkbook = 2;
                Excel.Workbook workBook = this.exelApp.Workbooks.Add(Type.Missing);
                this.exelApp.DisplayAlerts = false;
                this.sheet = (Excel.Worksheet)this.exelApp.Worksheets.get_Item(1);
                this.sheet.Name = "Протокол испытаний";

                this.LastUsedColumnsSignature = string.Empty;
                int rowNum = 1;
                int columnEnd = 0;

                //счётчик выведенных записей
                int counter = 0;

                //здесь будем хранить флаг о неизменности ПЗ во всём выведенном отчёте
                bool groupNameChanged = false;

                //здесь будем хранить предыдущий просмотренный в цикле GroupName
                string lastGroupName = null;

                //счётчики успешного и не успешного прохождения тестов
                int statusOKCount = 0;
                int statusFaultCount = 0;
                int statusUnknownCount = 0;
                int totalCount = 0;

                int lastUsedColumn = 0;
                int rowNumBeg;

                //храним здесь сколько шапок было выведено
                int needHeaderCount = 0;

                ListOfCalculatorsMinMax listOfCalculatorsMinMax = new ListOfCalculatorsMinMax();

                foreach (ReportData p in this)
                {
                    string currentGroupName = (p.RTData == null) ? p.TMData.GroupName : p.RTData.GroupName;

                    if ((!groupNameChanged) && (lastGroupName != null))
                        groupNameChanged = (currentGroupName != lastGroupName);

                    lastGroupName = currentGroupName;

                    int column = 1;

                    bool needHeader = ((this.LastUsedColumnsSignature == string.Empty) || (this.LastUsedColumnsSignature != p.ColumnsSignature));

                    //выводим шапку если имеет место смена списка выведенных столбцов
                    int headerRowNum = rowNum + 2;
                    rowNumBeg = rowNum;

                    if (needHeader)
                    {
                        columnEnd = column + 3;

                        //выводим самую верхнюю часть шапки
                        p.TopHeaderToExcel(this.exelApp, this.sheet, ref rowNum);

                        //выводим шапку столбцов условий и измеренных параметров
                        p.ColumnsHeaderToExcel(this.exelApp, this.sheet, ref rowNum, ref columnEnd);

                        //выводим шапку идентификационных данных
                        p.HeaderToExcel(this.exelApp, this.sheet, ref rowNum, column + 2, ref columnEnd);

                        //выводим шапку Pair
                        p.PairColumnsHeaderToExcel(this.exelApp, this.sheet, rowNumBeg + 3, ref columnEnd);

                        //выводим шапку идентификационных данных Pair
                        p.PairHeaderToExcel(this.exelApp, this.sheet, rowNum, ref columnEnd);

                        //выводим шапку статуса
                        p.StatusHeaderToExcel(this.exelApp, this.sheet, rowNum, columnEnd);

                        needHeaderCount++;
                        rowNum++;
                    }

                    //выводим идентификационные данные
                    counter++;
                    p.IdentityToExcel(this.exelApp, this.sheet, counter, rowNum, ref column);

                    //выводим тело
                    p.BodyToExcel(this.exelApp, this.sheet, listOfCalculatorsMinMax, rowNum, ref column);

                    //выводим конечные идентификационные данные
                    p.EndIdentityToExcel(this.exelApp, this.sheet, rowNum, ref column);

                    //выводим тело pair
                    p.PairBodyToExcel(this.exelApp, this.sheet, listOfCalculatorsMinMax, rowNum, ref column);

                    //выводим идентификационные данные pair
                    p.PairIdentityToExcel(this.exelApp, this.sheet, rowNum, column);

                    //выводим статус
                    bool? isStatusOK = null;
                    lastUsedColumn = p.StatusToExcel(this.exelApp, this.sheet, rowNum, columnEnd, ref isStatusOK);

                    //формируем значения счётчиков неопределённого/успешного/не успешного прохождения тестов
                    if (isStatusOK == null)
                        statusUnknownCount++;
                    else
                    {
                        switch (isStatusOK)
                        {
                            case false:
                                statusFaultCount++;
                                break;

                            default:
                                statusOKCount++;
                                break;
                        }
                    }

                    //считаем сколько всего изделий просмотрено в цикле
                    totalCount++;

                    //обводим границы
                    p.SetBorders(this.exelApp, this.sheet, rowNumBeg, rowNum, lastUsedColumn);

                    //запоминаем набор столбцов, которые мы вывели
                    this.LastUsedColumnsSignature = p.ColumnsSignature;

                    rowNum++;
                }

                //получаем количество ТМЦ, запущенных по ПЗ
                ReportData rd = this[0];

                //если на протяжении всего цикла не зафиксировано изменение ПЗ - выводим кол-во запущенных ТМЦ по ПЗ. если же изменение ПЗ зафиксировано - в отчёте есть данные по разным ПЗ и запущенное кол-во не имеет смысла
                if (!groupNameChanged)
                {
                    int? qtyReleased = rd?.QtyReleasedByGroupName(lastGroupName, connection);

                    //выводим значения min/max для определённых в listOfCalculatorsMinMax параметров
                    if (needHeaderCount == 1)
                        rd?.ListOfCalculatorsMinMaxToExcel(this.exelApp, this.sheet, rowNum, listOfCalculatorsMinMax);

                    //выводим количество ТМЦ, запущенных по ПЗ
                    rd?.QtyReleasedByGroupNameToExcel(this.exelApp, this.sheet, ref rowNum, lastGroupName, qtyReleased);

                    //выводим кол-во годных/не годных
                    rd?.QtyOKFaultToExcel(this.exelApp, this.sheet, rowNum, totalCount, statusUnknownCount, statusFaultCount, statusOKCount);
                }

                //создаём нижний колонтитул
                this.sheet.PageSetup.RightFooter = "Лист &P Листов &N";

                this.sheet.UsedRange.EntireRow.AutoFit();
                this.sheet.UsedRange.EntireColumn.AutoFit();
                this.sheet.UsedRange.Font.Name = "Arial Narrow";

                //настраиваем вид печатного отчёта
                this.sheet.PageSetup.LeftMargin = 0;
                this.sheet.PageSetup.RightMargin = 0;
                this.sheet.PageSetup.TopMargin = 42;
                this.sheet.PageSetup.BottomMargin = 28;
                this.sheet.PageSetup.Orientation = Excel.XlPageOrientation.xlLandscape;
                this.sheet.PageSetup.PaperSize = Excel.XlPaperSize.xlPaperA4;
                this.sheet.PageSetup.Zoom = false;
                this.sheet.PageSetup.FitToPagesWide = 1;
                this.sheet.PageSetup.FitToPagesTall = false;
                this.sheet.PageSetup.ScaleWithDocHeaderFooter = true;
                this.sheet.PageSetup.AlignMarginsHeaderFooter = true;
            }

            finally
            {
                if (visibleAfterBuilding)
                {
                    this.exelApp.Visible = visibleAfterBuilding;
                    this.exelApp.WindowState = Microsoft.Office.Interop.Excel.XlWindowState.xlMaximized;
                }
            }
        }

        public void Print()
        {
            //печать уже сформированного отчёта
            this.sheet?.PrintOut(Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
        }
    }
    */

    public class CalculatorMinMax
    {
        private string FName = null;
        private string FUm = null;
        private uint? FColumn = null;
        private double? FMinValue = null;
        private double? FMaxValue = null;

        public CalculatorMinMax(string name)
        {
            this.FName = name;
        }

        public string Name
        {
            get
            {
                return this.FName;
            }
        }

        public string Um
        {
            get
            {
                return this.FUm;
            }
        }

        public uint? Column
        {
            get
            {
                return this.FColumn;
            }
        }

        public double? MinValue
        {
            get
            {
                return this.FMinValue;
            }
        }

        public double? MaxValue
        {
            get
            {
                return this.FMaxValue;
            }
        }

        public void Calc(uint column, string name, string um, string value)
        {
            if (name.Contains(this.FName))
            {
                if (double.TryParse(value, out double dValue))
                {
                    this.FMinValue = (this.FMinValue == null) ? dValue : Math.Min((double)this.FMinValue, dValue);
                    this.FMaxValue = (this.FMaxValue == null) ? dValue : Math.Max((double)this.FMaxValue, dValue);

                    //запоминаем номер столбца в отчёте Excel чтобы при выводе вычисленных min/max данных знать куда выводить вычисленные данные
                    if (this.FColumn == null)
                        this.FColumn = column;

                    if (this.FUm == null)
                        this.FUm = um;
                }
            }
        }
    }

    public class ListOfCalculatorsMinMax : List<CalculatorMinMax>
    {
        public ListOfCalculatorsMinMax()
        {
            this.Add(new CalculatorMinMax("VTM")); //UTM
            this.Add(new CalculatorMinMax("IGT"));
            this.Add(new CalculatorMinMax("VGT")); //UGT
        }

        public void Calc(uint column, string name, string um, string value)
        {
            foreach (CalculatorMinMax calculator in this)
                calculator.Calc(column, name, um, value);
        }
    }

    /*
    public class ListOfBannedNamesForUseInReport : List<string>
    {
        //тут храним (в верхнем регистре) список имён conditions и parameters которые не надо выводить в отчёт
        public ListOfBannedNamesForUseInReport()
        {
            this.Add("RTBVTCONDITIONS©BVT_VR");           //UBRmax

            this.Add("RTBVTCONDITIONS©BVT_UDSMURSM_VD");  //UDSM

            this.Add("RTBVTCONDITIONS©BVT_UDSMURSM_VR");  //URSM

            this.Add("RTBVTCONDITIONS©BVT_I");

            this.Add("RTBVTCONDITIONS©ATU_POWERVALUE");
        }

        public bool Use(string name)
        {
            //true - разрешено использование данного имени conditions/parameters в отчёте
            //false - в отчёте данное имя conditions/parameters использовать нельзя
            return !this.Contains(name);
        }
    }
    */

    public class ListOfImportantNamesInReport : List<string>
    {
        //тут храним список имён conditions и parameters которые важны для пользователя
        public ListOfImportantNamesInReport()
        {
            this.Add("RTUTM");
            this.Add("TMUTM");

            this.Add("RTUBO");
            this.Add("TMUBO");

            this.Add("RTUBR");
            this.Add("TMUBR");

            this.Add("RTIDRM");
            this.Add("TMIDRM");

            this.Add("RTIRRM");
            this.Add("TMIRRM");

            this.Add("RTIDSM");
            this.Add("TMIDSM");

            this.Add("RTIRSM");
            this.Add("TMIRSM");
        }
    }
}

