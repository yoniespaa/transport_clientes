//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TransPortClientes.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Pilotos
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Pilotos()
        {
            this.Solicitudes_Transporte = new HashSet<Solicitudes_Transporte>();
        }
    
        public int id_piloto { get; set; }
        public string nombre_completo { get; set; }
        public string nombre_usuario { get; set; }
        public string pass { get; set; }
        public bool activo { get; set; }
        public bool eliminado { get; set; }
        public int id_usuario_creacion { get; set; }
        public Nullable<int> id_usuario_modificacion { get; set; }
        public Nullable<int> id_usuario_eliminacion { get; set; }
        public System.DateTime fecha_creacion { get; set; }
        public Nullable<System.DateTime> fecha_modificacio { get; set; }
        public Nullable<System.DateTime> fecha_eliminacion { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Solicitudes_Transporte> Solicitudes_Transporte { get; set; }
    }
}
