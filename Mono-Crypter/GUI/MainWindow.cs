using System;
using Gtk;
using GUI;

public partial class MainWindow : Gtk.Window
{
	#region [Properties]
	protected bool isEditing;
	#endregion
	
	#region [Constructors]
	public MainWindow () : base(Gtk.WindowType.Toplevel)
	{
		Build ();
		isEditing = false;
		
		//Le agrego un filtro al Seleccionador de archivos
		FileFilter filter  = new FileFilter();
    	filter.Name = "Archivos de configuración";
    	filter.AddPattern("*.conf");
    	fileChooser.AddFilter(filter);
	}
	#endregion
	
	#region [Methods]
	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		if (Util.ShowConfirmMessage("Salir", "Se perderán todos los cambios realizados:\n" +
										"¿Seguro que desea salir?", this))
		{
			Application.Quit ();
			a.RetVal = true;
		}
	}
	
	protected virtual void OnTxtFileNameTextInserted (object o, Gtk.TextInsertedArgs args)
	{
		btnNewFile.Sensitive = true;
	}
	
	protected virtual void OnFileChooserSelectionChanged (object sender, System.EventArgs e)
	{
		btnDecrypt.Sensitive = true;
	}
	
	protected virtual void OnBtnDecryptClicked (object sender, System.EventArgs e)
	{
		if (!isEditing)
			Decrypt();
		else if (Util.ShowConfirmMessage("Datos desencriptados", "Se perderán todos los cambios realizados.\n" +
											"¿Desea cargar los datos nuevamente?", this))
			Decrypt();
		else
			statusBar.Pop(1);
	}
	
	protected virtual void OnBtnNewFileClicked (object sender, System.EventArgs e)
	{
		//                                        A partir de esto, es independiente del OS
		string path = folderChooser.CurrentFolder + System.IO.Path.DirectorySeparatorChar + txtFileName.Text.Trim() + ".conf";
		
		if (Util.esCampoVacio(txtFileName.Text.Trim()))
		{
			Util.ShowErrorMessage("Campo vacío", "El nombre del archivo no puede estar vacío.", this);
			txtFileName.HasFocus = true;
		}
		else if (!Util.NewFile(path))
		{
			Util.ShowErrorMessage("Archivo duplicado", "Ya existe un archivo con ese nombre", this);
			txtFileName.HasFocus = true;
		}	
		else
		{
			folderChooser.Sensitive = false;
			txtFileName.Sensitive = false;
			btnNewFile.Sensitive = false;
			
			txvConn.Sensitive = true;
			txvConn.Buffer.Text = "";
			txtUser.Sensitive = true;
			txtPass.Sensitive = true;
			txtUser.Text = "";
			txtPass.Text = "";
			
			btnEncriptar.Sensitive = true;
			
			fileChooser.SetFilename(path);
			
			//Util.EncryptData(GetData(), fileChooser.Filename);
			
			isEditing = true;
			
			statusBar.Pop(1);
			statusBar.Push(1, "Archivo creado correctamente. Pulse <Encriptar> para guardar los cambios.");
		}
	}
	
	protected virtual void OnBtnEncriptarClicked (object sender, System.EventArgs e)
	{
		Util.EncryptData(GetData(), fileChooser.Filename);
		
		folderChooser.Sensitive = true;
		txtFileName.Sensitive = true;
		btnNewFile.Sensitive = true;
		
		txvConn.Sensitive = false;
		txvConn.Buffer.Text = "";
		txtUser.Sensitive = false;
		txtPass.Sensitive = false;
		txtUser.Text = "";
		txtPass.Text = "";
		
		btnEncriptar.Sensitive = false;
		isEditing = false;	
		
		statusBar.Pop(1);
		statusBar.Push(1, "Datos encriptados correctamente.");
		
	}
	
	protected void Decrypt ()
	{
		string[] data = Util.DecryptFile(fileChooser.Filename);
		
		folderChooser.Sensitive = false;
		txtFileName.Sensitive = false;
		btnNewFile.Sensitive = false;
		
		txvConn.Sensitive = true;
		txvConn.Buffer.Text = data[0];
		txtUser.Sensitive = true;
		txtPass.Sensitive = true;
		txtUser.Text = data[1];
		txtPass.Text = data[2];
		
		btnEncriptar.Sensitive = true;
		isEditing = true;
		
		statusBar.Pop(1);
		statusBar.Push(1, "Datos desencriptados correctamente. Pulse <Encriptar> para guardar los cambios.");
	}
		
	protected string GetData()
	{
		return (txvConn.Buffer.Text.Trim() + Util.spliter + txtUser.Text.Trim() + Util.spliter + txtPass.Text + Util.spliter);
	}
	#endregion
}

