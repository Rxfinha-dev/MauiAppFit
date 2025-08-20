using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MauiAppFit.ViewModels
{
    [QueryProperty("PegarIdDaNavegacao", "parametro_id")]
    public class CadastroAtividadeViewModel : INotifyPropertyChanged
    {
        public event PropertyChangingEventHandler PropertyChanged;

        string descricao, observacoes;
        int id;
        DateTime data;
        double? peso;

        public string PegarIdDaNavegacao
        {
            set
            {
                int id_parametro = Convert.ToInt32(
                        Uri.UnescapeDataString(value));

                VerAtividade.Execute(id_parametro);

                
            }
        }

        public string Descricao
        {
            get=> descricao;
            set
            {
                descricao = value;
                PropertyChanged(this,
                    new PropertyChangedEventArgs("Descricao"));
            }
        }
        public string Observacoes
        {
            get => observacoes;
            set { 
                observacoes = value;
                PropertyChanged(this,
                    new PropertyChangedEventArgs("Observacoes"));

            }
        }

        public int Id
        {
            get => id;
            set {  id = value;
                PropertyChanged(this, 
                    new PropertyChangedEventArgs("id"));
            }
        }

        public DateTime Date
        {
            get => data;
            set
            {
                data = value;
                PropertyChanged(this, 
                    new PropertyChangedEventArgs("Data"));
            }
        }

        public double? Peso
        {
            get => peso;
            set
            {
                peso = value;
                PropertyChanged(this,
                    new PropertyChangedEventArgs("Peso"));

            }
        }

               public ICommand NovaAtividade
        {
            get => new Command(() =>
            {
                Id = 0;
                Descricao = String.Empty;
                Data = DateTime.Now;
                Peso = null;
                Observacoes = String.Empty;
            });
        }

        public ICommand SalvarAtividade
        {
            get => new Command(async () =>
            {
                try
                {
                    Atividade model = new Atividade()
                    {
                        Descricao = this.Descricao,
                        Data = this.Data,
                        Peso = this.Peso,
                        Observacoes = this.Observacoes
                    };

                    if (this.Id == 0)
                    {
                        await App.Database.Insert(model);

                    }
                    else
                    {
                        model.Id = this.Id;

                        await App.Database.Update(model);
                    }

                    await Application.Current.MainPage.DisplayAlert("Beleza!", "Atividade Salva!", "OK");

                    await Shell.Current.GoToAsync("//MinhasAtividades");

                }
                catch (Exception ex)
                {
                    await Application.Current.MainPage.DisplayAlert("Ops", ex.Message, "OK");
                }
            });
        }


        public ICommand VerAtividade
        {
            get => new Command<int>(async (int id) =>
            {
                try
                {
                    Atividade model = await App.Database.GetById(id);

                    this.Id = model.Id;
                    this.Descricao = model.Descricao;
                    this.Peso = model.Peso;
                    this.Data = model.Data;
                    this.Observacoes = model.Observacoes;
                }
                catch (Exception ex)
                {
                    await Application.Current.MainPage.DisplayAlert("Ops", ex.Message, "OK");
                }
            });
        }
    }
}
