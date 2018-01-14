﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using NBP_Neo4j_Redis.Controllers;
using NBP_Neo4j_Redis.Adapters;
using Android.Graphics;
using NBP_Neo4j_Redis.NecessaryClasses;
using CombinedAPI.Entities;

namespace NBP_Neo4j_Redis.Activities
{
    [Activity(Theme = "@android:style/Theme.NoTitleBar", Icon = "@drawable/user", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class ProfileActivity : Activity
    {
        ImageView _profilnaSlika;
        ImageView _imageEdit;
        ImageView _imageOk;
        TextView _textImePrezime;
        TextView _textDatumRodjenja;
        TextView _textMestoStanovanja;
        TextView _textPol;
        ListView _listaSlika;

        EditText _editDatumRodjenja;
        EditText _editPol;
        EditText _editMestoStanovanja;

        Button _btnDodateFotografije;
        Button _btnOznaceneFotografije;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Profile);

            PoveziKomponente();
            UcitajProfilnePodatke();
            
            OsposobiAdapter(DataController.Instance.OdabraniProfil.DodateSlike);
            PrikaziPoljaZaPregled();
        }
        public void PoveziKomponente()
        {

            _profilnaSlika = FindViewById<ImageView>(Resource.Id.PprofilnaSlika);
            _textImePrezime = FindViewById<TextView>(Resource.Id.TextViewImePrezime);
            _textDatumRodjenja = FindViewById<TextView>(Resource.Id.TextViewDatumRodjenja);
            _textMestoStanovanja = FindViewById<TextView>(Resource.Id.TextViewMestoStanovanja);
            _textPol = FindViewById<TextView>(Resource.Id.TextViewPol);
            _listaSlika = FindViewById<ListView>(Resource.Id.ListViewImages);
            _editDatumRodjenja = FindViewById<EditText>(Resource.Id.EditTextDatumRodjenja);
            _editMestoStanovanja = FindViewById<EditText>(Resource.Id.EditTextMestoStanovanja);
            _editPol = FindViewById<EditText>(Resource.Id.EditTextPol);

            _imageEdit = FindViewById<ImageView>(Resource.Id.imageViewEdit);
            _imageOk = FindViewById<ImageView>(Resource.Id.imageViewOk);
            _imageEdit.Click += _imageEdit_Click;
            _imageOk.Click += _imageOk_Click;

            _btnDodateFotografije = FindViewById<Button>(Resource.Id.buttonIzbaceneSlike);
            _btnOznaceneFotografije = FindViewById<Button>(Resource.Id.buttonOznaceneSlike);
            _btnOznaceneFotografije.Click += _btnOznaceneFotografije_Click;
            _btnDodateFotografije.Click += _btnDodateFotografije_Click;
        }

        private void _btnDodateFotografije_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void _btnOznaceneFotografije_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void _imageOk_Click(object sender, EventArgs e)
        {
            PrikaziPoljaZaPregled();
        }

        private void _imageEdit_Click(object sender, EventArgs e)
        {
            PrikaziPoljaZaEditovanje();
        }

        public void UcitajProfilnePodatke()
        {
            if (DataController.Instance.OdabraniProfil.Profilna.Sadrzaj != null)
            {
                
                Bitmap bitmap = BitmapConverter.ConvertStringToBitmap(DataController.Instance.OdabraniProfil.Profilna.Sadrzaj);
                _profilnaSlika.SetImageBitmap(bitmap);
            }
            string imePrezime = DataController.Instance.OdabraniProfil.Ime + " " + DataController.Instance.OdabraniProfil.Prezime;

            _textDatumRodjenja.Text = DataController.Instance.OdabraniProfil.DatumRodjenja.ToShortDateString();
            _textMestoStanovanja.Text = DataController.Instance.OdabraniProfil.MestoStanovanja;
            _textPol.Text = DataController.Instance.OdabraniProfil.Pol;

        }

        public void OsposobiAdapter(List<Slika> slike)
        {
            List<TwoImages> _listOfTwoImages = new List<TwoImages>();
            for (int i = 0; i < slike.Count / 2; i++)
            {
                TwoImages images = new TwoImages();
                images.Slika1 = slike[i * 2];
                images.Slika2 = slike[i * 2 + 1];
                _listOfTwoImages.Add(images);
            }
            if (slike.Count % 2 != 0)
            {
                TwoImages images = new TwoImages();
                images.Slika1 = slike[slike.Count - 1];
                _listOfTwoImages.Add(images);
            }

            _listaSlika.Adapter = new UserImagesAdapter(this, _listOfTwoImages);
        }
        
        public void OsposobiProbniAdapter()
        {
            List<TwoImages> slike = new List<TwoImages>();
            for (int i = 0; i < 10; i++)
            {
                slike.Add(new TwoImages());
            }
            _listaSlika.Adapter = new UserImagesAdapter(this, slike);
        }

        public void PrikaziPoljaZaEditovanje()
        {
            _textDatumRodjenja.Visibility = ViewStates.Invisible;
            _textMestoStanovanja.Visibility = ViewStates.Invisible;
            _textPol.Visibility = ViewStates.Invisible;
            _editPol.Visibility = ViewStates.Visible;
            _editDatumRodjenja.Visibility = ViewStates.Visible;
            _editMestoStanovanja.Visibility = ViewStates.Visible;

            _imageOk.Visibility = ViewStates.Visible;
            _imageEdit.Visibility = ViewStates.Invisible;

            _textDatumRodjenja.Text = DataController.Instance.OdabraniProfil.DatumRodjenja.ToShortDateString();
            _textMestoStanovanja.Text = DataController.Instance.OdabraniProfil.MestoStanovanja;
            _textPol.Text = DataController.Instance.OdabraniProfil.Pol;

        }

        public void PrikaziPoljaZaPregled()
        {
            _textDatumRodjenja.Visibility = ViewStates.Visible;
            _textMestoStanovanja.Visibility = ViewStates.Visible;
            _textPol.Visibility = ViewStates.Visible;
            _editPol.Visibility = ViewStates.Invisible;
            _editDatumRodjenja.Visibility = ViewStates.Invisible;
            _editMestoStanovanja.Visibility = ViewStates.Invisible;

            _imageOk.Visibility = ViewStates.Invisible;
            _imageEdit.Visibility = ViewStates.Visible;

           // PrikazKorisnikovogProfila();
        }

        public void PrikazKorisnikovogProfila()
        {
            if (SignLogInController.Instance.MojProfil.KorisnickoIme == DataController.Instance.OdabraniProfil.KorisnickoIme)
            {
                _imageEdit.Visibility = ViewStates.Visible;
                //dodati deo za klik na profilnu
            }
            else
            {
                //skloniti event handler-e sa slike
            }
        }

    }
}