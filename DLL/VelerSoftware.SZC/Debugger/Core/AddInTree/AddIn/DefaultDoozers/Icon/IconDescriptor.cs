﻿// *****************************************************************************
// 
//  © Veler Software 2012. All rights reserved.
//  The current code and the associated software are the proprietary 
//  information of Etienne Baudoux from Veler Software and are
//  supplied subject to licence terms.
// 
//  www.velersoftware.com
// *****************************************************************************




namespace VelerSoftware.SZC.Debugger.Core
{
    public class IconDescriptor
    {
        Codon codon;

        public string Id
        {
            get
            {
                return codon.Id;
            }
        }

        public string Language
        {
            get
            {
                return codon.Properties["language"];
            }
        }

        public string Resource
        {
            get
            {
                return codon.Properties["resource"];
            }
        }

        public string[] Extensions
        {
            get
            {
                return codon.Properties["extensions"].Split(';');
            }
        }

        public IconDescriptor(Codon codon)
        {
            this.codon = codon;
        }
    }
}