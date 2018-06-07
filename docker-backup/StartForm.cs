using Docker.DotNet;
using Docker.DotNet.BasicAuth;
using Docker.DotNet.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace docker_backup
{
    public partial class StartForm : Form
    {
        public DockerClient dockerclient;

        public StartForm()
        {
            InitializeComponent();
        }

        private void basicAuthCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (basicAuthCheckbox.Checked)
            {
                usernameLabel.Visible = true;
                usernameEntry.Visible = true;
                passwordLabel.Visible = true;
                passwordEntry.Visible = true;
            } else
            {
                usernameLabel.Visible = false;
                usernameEntry.Visible = false;
                passwordLabel.Visible = false;
                passwordEntry.Visible = false;
            }
        }

        private async void testConnectivityButton_Click(object sender, EventArgs e)
        {
            listViewContainers.Items.Clear();

            if (basicAuthCheckbox.Checked)
            {
                dockerclient = new DockerClientConfiguration(new Uri(dockerEndpointEntry.Text), new BasicAuthCredentials(usernameEntry.Text, passwordEntry.Text)).CreateClient();
            } else
            {
                dockerclient = new DockerClientConfiguration(new Uri(dockerEndpointEntry.Text)).CreateClient();
            }

            try
            {
                IList<ContainerListResponse> containers = await dockerclient.Containers.ListContainersAsync(
                    new ContainersListParameters()
                    {
                        Limit = 1000,
                    }
                );

                foreach (var c in containers)
                {
                    ListViewItem currentList = new ListViewItem(c.Names[0]);
                    currentList.SubItems.Add(c.ID);
                    listViewContainers.Items.Add(currentList);
                }

                listViewContainers.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            } catch
            {
                MessageBox.Show("Can't fetch containers from the given endpoint.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void StartForm_Load(object sender, EventArgs e)
        {

        }

        private async void listViewContainers_DoubleClick(object sender, EventArgs e)
        {
            String containerName = listViewContainers.SelectedItems[0].SubItems[0].Text;
            String containerID   = listViewContainers.SelectedItems[0].SubItems[1].Text;

            Stream containerStream = await dockerclient.Containers.ExportContainerAsync(containerID);

            SaveFileDialog exportFileDialog = new SaveFileDialog();
            exportFileDialog.Filter = "tarball (*.tar)|*.tar";
            
            if (exportFileDialog.ShowDialog() == DialogResult.OK)
            {
                var fileStream = File.Create(exportFileDialog.FileName);
                containerStream.CopyTo(fileStream);
                fileStream.Close();
                MessageBox.Show("Done!", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
