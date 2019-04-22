//namespace Mtf.Hardware.Raid.Amcc3Ware
//{
//    public class GraphicalDisplay
//    {
//        public TreeNode GetNodeStructure()
//        {
//            var result = new TreeNode(Constants.RAID, Constants.RAID_MINI_ICONINDEX, Constants.RAID_MINI_ICONINDEX);
//
//            for (var i = 0; i < Controllers.Length; i++)
//            {
//                var controller = Controllers[i];
//                var image_index = controller.NumberOfNotOptimalUnits == 0 ? Constants.RAID_OK_ICONINDEX : Constants.RAID_ERROR_ICONINDEX;
//                result.Nodes.Add(controller.Name, controller.Name /*String.Format("{0} ({1})", controller.Name, controller.Model)*/, image_index, image_index);
//                var controller_node = result.Nodes[i];
//                controller_node.Tag = controller;
//
//                for (var j = 0; j < controller.Units.Length; j++)
//                {
//                    var unit = controller.Units[j];
//
//                    //string node_name = String.Format("{0} ({1} - {2} {3})", unit.UnitName, unit.Name, unit.Status, unit.GetRebuildStatus());
//                    switch (unit.Status)
//                    {
//                        case Constants.OK:
//                            image_index = Constants.RAID_OK_ICONINDEX;
//                            break;
//                        case Constants.REBUILDING:
//                            image_index = Constants.RAID_UNIT_REBUILD_ICONINDEX;
//                            break;
//                        case Constants.VERIFYING:
//                            image_index = Constants.RAID_UNIT_VERIFY_ICONINDEX;
//                            break;
//                        default:
//                            image_index = Constants.RAID_ERROR_ICONINDEX;
//                            break;
//                    }
//                    //image_index = unit.OK ? Constants.RAID_OK_ICONINDEX : Constants.RAID_ERROR_ICONINDEX;
//                    controller_node.Nodes.Add(unit.UnitName, $"{unit.UnitName} ({unit.Name})", image_index, image_index);
//                    var unitNode = controller_node.Nodes[j];
//                    unitNode.Tag = unit;
//                    //unit_node.ImageIndex =
//                    //unit_node.SelectedImageIndex = unit_node.ImageIndex;
//
//                    foreach (var subunit in unit.SubUnits)
//                    {
//                        if (subunit.UnitType != Constants.VOLUME)
//                            continue;
//
//                        if (!unitNode.Nodes.ContainsKey(Constants.VOLUME))
//                        {
//                            unitNode.Nodes.Add(Constants.VOLUME, Constants.VOLUME, Constants.RAID_VOLUME_ICONINDEX, Constants.RAID_VOLUME_ICONINDEX);
//                        }
//                        var volumeNode = unitNode.Nodes[Constants.VOLUME];
//                        volumeNode.Nodes.Add(subunit.UnitName, subunit.UnitName);
//                        //volumeNode.Nodes.Add(subunit.UnitName, String.Format("{0} {1}", subunit.UnitName, subunit.Status));
//                        var subunitNode = volumeNode.Nodes[subunit.UnitName];
//                        subunitNode.Tag = subunit;
//                        subunitNode.ImageIndex = subunit.Status == "-" ? Constants.RAID_NO_ICON : subunit.OK ? Constants.RAID_OK_ICONINDEX : Constants.RAID_ERROR_ICONINDEX;
//                        subunitNode.SelectedImageIndex = subunitNode.ImageIndex;
//                    }
//                }
//
//                for (var j = 0; j < controller.Ports.Length; j++)
//                {
//                    var port = controller.Ports[j];
//                    var unitNode = controller_node.Nodes[port.Unit];
//                    if (!unitNode.Nodes.ContainsKey(Constants.HDDS)) unitNode.Nodes.Add(Constants.HDDS, Constants.HDDS, Constants.RAID_DRIVE_ICONINDEX, Constants.RAID_DRIVE_ICONINDEX);
//
//                    unitNode.Nodes[Constants.HDDS].Nodes.Add(port.PortName, port.PortName /*String.Format("{0} {1}", port.PortName, port.Status)*/);
//                    var portNode = unitNode.Nodes[Constants.HDDS].Nodes[port.PortName];
//                    portNode.Tag = port;
//                    portNode.ImageIndex = port.OK ? Constants.RAID_OK_ICONINDEX : Constants.RAID_ERROR_ICONINDEX;
//                    portNode.SelectedImageIndex = portNode.ImageIndex;
//                }
//            }
//
//            return result;
//        }
//
//        public void ChangeStatesOnNodeStructure(TreeNode node)
//        {
//            //TreeNode root = ;
//            for (var i = 0; i < Controllers.Length; i++)
//            {
//                var controller = Controllers[i];
//                var controllerNode = node.Nodes[controller.Name];
//                for (var j = 0; j < controller.Units.Length; j++)
//                {
//                    var unit = Controllers[i].Units[j];
//                    var unitNode = controllerNode.Nodes[j];
//
//                    int imageIndex;
//                    switch (unit.Status)
//                    {
//                        case Constants.OK:
//                            imageIndex = Constants.RAID_OK_ICONINDEX;
//                            break;
//                        case Constants.REBUILDING:
//                            imageIndex = Constants.RAID_UNIT_REBUILD_ICONINDEX;
//                            break;
//                        case Constants.VERIFYING:
//                            imageIndex = Constants.RAID_UNIT_VERIFY_ICONINDEX;
//                            break;
//                        default:
//                            imageIndex = Constants.RAID_ERROR_ICONINDEX;
//                            break;
//                    }
//                    //unitNode.ImageIndex = unit.OK ? Constants.RAID_OK_ICONINDEX : Constants.RAID_ERROR_ICONINDEX;
//                    unitNode.ImageIndex = imageIndex;
//                    unitNode.SelectedImageIndex = unitNode.ImageIndex;
//
//                    foreach (var subunit in unit.SubUnits)
//                    {
//                        if (subunit.UnitType != Constants.VOLUME)
//                            continue;
//
//                        var subunitNode = unitNode.Nodes[Constants.VOLUME].Nodes[subunit.UnitName];
//                        if (subunit.Status == "-") subunitNode.ImageIndex = Constants.RAID_NO_ICON;
//                        else subunitNode.ImageIndex = subunit.OK ? Constants.RAID_OK_ICONINDEX : Constants.RAID_ERROR_ICONINDEX;
//                        subunitNode.SelectedImageIndex = subunitNode.ImageIndex;
//                    }
//                }
//
//                for (var j = 0; j < Controllers[i].Ports.Length; j++)
//                {
//                    var port = controller.Ports[j];
//
//                    /*TreeNode unit_node = controller_node.Nodes[port.Unit];
//                    TreeNode hdds_node = unit_node.Nodes[Constants.HDDS];
//                    TreeNode port_node = hdds_node.Nodes[port.PortName];*/
//                    var portNode = controllerNode.Nodes[port.Unit].Nodes[Constants.HDDS].Nodes[port.PortName];
//
//                    portNode.ImageIndex = port.OK ? Constants.RAID_OK_ICONINDEX : Constants.RAID_ERROR_ICONINDEX;
//                    portNode.SelectedImageIndex = portNode.ImageIndex;
//                }
//            }
//        }
//
//        public HDD_States[] GetPortStates(EnclosureTypes type)
//        {
//            var HDD_index = 0;
//            var states = new HDD_States[RaidEnclosures.GetHDDCount(type)];
//
//            foreach (var controller in Controllers)
//            {
//                foreach (var port in controller.Ports)
//                {
//                    switch (port.Status)
//                    {
//                        case Constants.OK:
//                            if (Convert.ToInt32(port.Temperature.Replace("°C", String.Empty)) >= Constants.HOT_HDD) states[HDD_index++] = HDD_States.WARM;
//                            else states[HDD_index++] = HDD_States.OK;
//                            break;
//                        case Constants.WARNING:
//                            states[HDD_index++] = HDD_States.WARNING;
//                            break;
//                        case Constants.ERROR:
//                            states[HDD_index++] = HDD_States.ERROR;
//                            break;
//                        case Constants.REBUILDING:
//                            states[HDD_index++] = HDD_States.REBUILDING;
//                            break;
//                    }
//                }
//            }
//
//            while (HDD_index < states.Length) states[HDD_index++] = HDD_States.NOT_USED;
//            return states;
//        }
//
//
//
//        public static HDD_States[] GetHDDStatesFromToString(EnclosureTypes enclosure_type, string amcc_3ware_state_to_string)
//        {
//            var result = new HDD_States[RAID_Panel.GetHDDCount(enclosure_type)];
//            if ((amcc_3ware_state_to_string == String.Empty) || (amcc_3ware_state_to_string == Constants.NULL_STRING)) return result;
//            //throw new ArgumentException(Constants.PARAMETER_IS_EMPTY_STRING, amcc_3ware_state_to_string);
//
//            var lines = amcc_3ware_state_to_string.SplitOnNewLines();
//            int start_index = -1, end_index = -1;
//
//            for (var i = 0; i < lines.Length; i++)
//            {
//                if (lines[i] == Constants.PORTS) start_index = i + 2;
//                else if ((lines[i].Trim() == String.Empty) || (lines[i] == Constants.BBU)) end_index = i - 1;
//            }
//
//            var hdd_count = end_index - start_index + 1;
//
//            for (var i = 0; i < hdd_count; i++)
//            {
//                var index = lines[i + start_index].IndexOf(", Status ") + ", Status ".Length;
//                var length = lines[i + start_index].IndexOf(", Temp ", index + 1) - index;
//                if (length < 0) length = lines[i + start_index].Length - index;
//                else
//                {
//                    var index_2 = lines[i + start_index].IndexOf(", Temp ") + ", Temp ".Length;
//                    var length_2 = lines[i + start_index].IndexOf("°C", index_2 + 1) - index_2;
//                    var temperature = Convert.ToInt32(lines[i + start_index].Substring(index_2, length_2));
//                    if (temperature >= Constants.HOT_HDD) result[i] = HDD_States.HOT;
//                }
//                var state_string = lines[i + start_index].Substring(index, length);
//                if (result[i] != HDD_States.HOT)
//                    result[i] = (HDD_States)Enum.Parse(typeof(HDD_States), state_string);
//            }
//            return result;
//        }
//
//        public static TreeNode GetNodeStructure(string amcc_3ware_state_to_string)
//        {
//            if (amcc_3ware_state_to_string == Constants.NULL_STRING) return null;
//
//            var result = new TreeNode(Constants.RAID, Constants.RAID_MINI_ICONINDEX, Constants.RAID_MINI_ICONINDEX);
//            TreeNode controller_node = null;
//            var controller_index = 0;
//            var unit_index = 0;
//
//            var lines = amcc_3ware_state_to_string.SplitOnNewLines();
//
//            var i = 0;
//            while (i < lines.Length)
//            {
//                int image_index;
//                if (lines[i] == Constants.CONTROLLER)
//                {
//                    i += 2;
//                    var not_optimal_units = Convert.ToInt32(lines[i].Substring(lines[i].IndexOf(Constants.NOT_OPTIMAL_UNITS) + Constants.NOT_OPTIMAL_UNITS.Length + 1));
//
//                    image_index = (not_optimal_units == 0) ? Constants.RAID_OK_ICONINDEX : Constants.RAID_ERROR_ICONINDEX;
//                    //int index = Constants.NAME.Length + 2;
//                    //string controller_name = lines[i].Substring(String.Format("{0}: ", Constants.NAME));//.Trim(); c0
//                    var controller_name = lines[i].Substring(String.Format("{0}: ", Constants.NAME));//.Trim();
//                    //string controller_name = lines[i].Substring(index, lines[i].IndexOf(',', index) - index);
//                    result.Nodes.Add(controller_name, controller_name, image_index, image_index);
//                    result.Nodes[controller_index].Tag = controller_index;
//                    controller_node = result.Nodes[controller_index++];
//                }
//                else
//                {
//                    TreeNode unit_node;
//                    switch (lines[i])
//                    {
//                        case Constants.UNITS:
//                            i += 2;
//                            while (lines[i] != Constants.PORTS)
//                            {
//                                if (lines[i] != String.Empty)
//                                {
//                                    /*int s_index = lines[i].IndexOf(Constants.STATUS) + Constants.STATUS.Length + 1;
//                            int e_index = lines[i].IndexOf(',', s_index);
//                            string status = lines[i].Substring(s_index, e_index - s_index);*/
//                                    var status = lines[i].Substring(Constants.STATUS).Trim();
//                                    var name = lines[i].Substring(String.Format(" {0}", Constants.NAME));
//                                    var unit_name = lines[i].Substring(Constants.UNITNAME);
//
//                                    switch (status)
//                                    {
//                                        case Constants.OK:
//                                            image_index = Constants.RAID_OK_ICONINDEX;
//                                            break;
//                                        case Constants.INITIALIZING:
//                                            image_index = Constants.RAID_UNIT_INITIALIZE_ICONINDEX;
//                                            break;
//                                        case Constants.REBUILDING:
//                                            image_index = Constants.RAID_UNIT_REBUILD_ICONINDEX;
//                                            break;
//                                        case Constants.VERIFYING:
//                                            image_index = Constants.RAID_UNIT_VERIFY_ICONINDEX;
//                                            break;
//                                        default:
//                                            image_index = Constants.RAID_ERROR_ICONINDEX;
//                                            break;
//                                    }
//                                    if (controller_node != null)
//                                    {
//                                        controller_node.Nodes.Add(unit_name, $"{unit_name} ({name})", image_index, image_index);
//                                        controller_node.Nodes[unit_index].Tag = unit_index;
//                                        unit_node = controller_node.Nodes[unit_index++];
//                                    }
//                                }
//                                i++;
//                            }
//                            i -= 2;
//                            break;
//                        case Constants.PORTS:
//                            i += 2;
//                            while (lines[i] != String.Empty)
//                            {
//                                var unit_name = lines[i].Substring(Constants.UNIT);
//
//                                int unitindex;
//                                try
//                                {
//                                    unitindex = Convert.ToInt32(unit_name.Substring(1));
//                                }
//                                catch (Exception)
//                                {
//                                    unitindex = -1;
//                                }
//
//                                var port_name = lines[i].Substring(Constants.PORTNAME);
//                                var status = lines[i].Substring(Constants.STATUS);
//
//                                int temperature;
//                                try
//                                {
//                                    temperature = Convert.ToInt32(lines[i].Substring(Constants.TEMP).Replace("°C", String.Empty));
//                                }
//                                catch (Exception)
//                                {
//                                    temperature = 0;
//                                }
//
//                                if ((controller_node != null) && (unitindex != -1))
//                                {
//                                    for (var j = 0; j < controller_node.Nodes.Count; j++)
//                                    {
//                                        var tag = Convert.ToInt32(controller_node.Nodes[j].Tag);
//
//                                        if (tag != unitindex)
//                                            continue;
//
//                                        if (!controller_node.Nodes[j].Nodes.ContainsKey(Constants.HDDS))
//                                            controller_node.Nodes[j].Nodes.Add(Constants.HDDS, Constants.HDDS, Constants.RAID_DRIVE_ICONINDEX,
//                                                Constants.RAID_DRIVE_ICONINDEX);
//                                        unit_node = controller_node.Nodes[j].Nodes[Constants.HDDS];
//                                        image_index = status == Constants.OK ? Constants.RAID_OK_ICONINDEX : Constants.RAID_ERROR_ICONINDEX;
//                                        unit_node.Nodes.Add(port_name, port_name, image_index, image_index);
//                                        break;
//                                        //unit_node.Nodes[unit_index].Tag = unit_index
//                                    }
//                                }
////PortName p0, Serial JK11H1B9HTLH3R, Unit u0, Status OK, Temp 32°C
//                                i++;
//                            }
//                            break;
//                    }
//                }
//                i++;
//
//                /*for (int j = 0; j < controller.Units.Length; j++)
//                {
//                    Unit unit = controller.Units[j];
//
//                    //string node_name = String.Format("{0} ({1} - {2} {3})", unit.UnitName, unit.Name, unit.Status, unit.GetRebuildStatus());
//
//                    //image_index = unit.OK ? Constants.RAID_OK_ICONINDEX : Constants.RAID_ERROR_ICONINDEX;
//                    controller_node.Nodes.Add(unit.UnitName, String.Format("{0} ({1})", unit.UnitName, unit.Name), image_index, image_index);
//                    TreeNode unit_node = controller_node.Nodes[j];
//                    unit_node.Tag = unit;
//                    //unit_node.ImageIndex =
//                    //unit_node.SelectedImageIndex = unit_node.ImageIndex;
//
//// Unit ToString
////if ((this.Name != null) && (this.Name != String.Empty))
////	return String.Format(base.ToString() + ", Name {0}, Serial {1}, Rebuild status {2}", this.Name, this.Serial, this.GetRebuildStatus());
////else
////	return String.Format(base.ToString() + ", Serial {0}, Rebuild status {1}", this.Serial, this.GetRebuildStatus());
//
//                    for (int k = 0; k < unit.SubUnits.Length; k++)
//                    {
////SubUnit ToString
////return String.Format("UnitName {0}, UnitType {1}, UnitVPort {2}, Status {3}", this.UnitName, this.UnitType, this.VPort, this.Status);
//
//                        SubUnit subunit = unit.SubUnits[k];
//                        if (subunit.UnitType == Constants.VOLUME)
//                        {
//                            if (!unit_node.Nodes.ContainsKey(Constants.VOLUME)) unit_node.Nodes.Add(Constants.VOLUME, Constants.VOLUME, Constants.RAID_VOLUME_ICONINDEX, Constants.RAID_VOLUME_ICONINDEX);
//                            TreeNode volume_node = unit_node.Nodes[Constants.VOLUME];
//                            volume_node.Nodes.Add(subunit.UnitName, subunit.UnitName);
//                            //volume_node.Nodes.Add(subunit.UnitName, String.Format("{0} {1}", subunit.UnitName, subunit.Status));
//                            TreeNode subunit_node = volume_node.Nodes[subunit.UnitName];
//                            subunit_node.Tag = subunit;
//                            if (subunit.Status == "-") subunit_node.ImageIndex = Constants.RAID_NO_ICON;
//                            else subunit_node.ImageIndex = subunit.OK ? Constants.RAID_OK_ICONINDEX : Constants.RAID_ERROR_ICONINDEX;
//                            subunit_node.SelectedImageIndex = subunit_node.ImageIndex;
//                        }
//                    }
//                }
//
//                for (int j = 0; j < controller.Ports.Length; j++)
//                {
//                    Port port = controller.Ports[j];
//                    TreeNode unit_node = controller_node.Nodes[port.Unit];
//                    if (!unit_node.Nodes.ContainsKey(Constants.HDDS)) unit_node.Nodes.Add(Constants.HDDS, Constants.HDDS, Constants.RAID_DRIVE_ICONINDEX, Constants.RAID_DRIVE_ICONINDEX);
//
//                    unit_node.Nodes[Constants.HDDS].Nodes.Add(port.PortName, port.PortName);
//                    TreeNode port_node = unit_node.Nodes[Constants.HDDS].Nodes[port.PortName];
//                    port_node.Tag = port;
//                    port_node.ImageIndex = port.OK ? Constants.RAID_OK_ICONINDEX : Constants.RAID_ERROR_ICONINDEX;
//                    port_node.SelectedImageIndex = port_node.ImageIndex;
//                }/**/
//            }
//
//            return result;
//        }
//
//    }
//}