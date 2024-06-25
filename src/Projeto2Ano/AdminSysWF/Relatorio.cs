using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminSysWF
{
    public class Relatorio
    {
        public class Lucros
        {
            public float LucroSemanal;
            public float LucroMensal;
            public float LucroUltimoMes;
            public string mediaDiaria;
        }

        public class Ganhos
        {
            public float GanhosMensal;
            public float GanhosUltimoMes;
            public string TaxaEmRelacaoAoUltimoMes;
            public string mediaDiaria;
        }

        public class Despesas
        {
            public float DespesasMensal;
            public float DespesasUltimoMes;
            public float TaxaEmRelacaoAoUltimoMes;
            public string mediaDiaria;
        }

        public class Investimentos
        {
            public float ValorInicial;
            public float ValorTotal;
            public string TaxaVariacao;
            public string MelhorAtivo;
            public string PiorAtivo;
        }
        
    }
}
