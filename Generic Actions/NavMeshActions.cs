﻿using UnityEngine;
using System.Collections;

namespace Hivemind {

	public class NavMeshActions : ActionLibrary {
		
		[Hivemind.Action]
		[Hivemind.Expects("gameObject", typeof(GameObject))]
		public Hivemind.Status MoveToGameObject() {
			GameObject go = context.Get<GameObject> ("gameObject");
			
			NavMeshAgent navMeshAgent = agent.GetComponent<NavMeshAgent>();
			Animator anim = agent.GetComponent<Animator>();
			NavMeshHit sampledDestination;
			NavMesh.SamplePosition(go.transform.position, out sampledDestination, 10f, 1);
			float distance = Vector3.Distance (agent.transform.position, sampledDestination.position);
			
			// Planning path or moving towards destination
			if (navMeshAgent.pathPending || navMeshAgent.hasPath && distance > navMeshAgent.stoppingDistance) {
				return Status.Running;
			}
			
			// Reached destination
			else if (distance <= navMeshAgent.stoppingDistance) {
				anim.SetBool ("IsWalking", false);
				return Status.Success;
			}
			
			// Can't reach destination
			else if (navMeshAgent.pathStatus == NavMeshPathStatus.PathInvalid) {
				return Status.Failure;
			}
			
			// Set destination
			else {
				navMeshAgent.SetDestination(sampledDestination.position);
				anim.SetBool ("IsWalking", true);
				return Status.Running;
			}
		}

		[Hivemind.Action]
		[Hivemind.Expects("position", typeof(Vector3))]
		public Hivemind.Status MoveTo() {
			
			NavMeshAgent navMeshAgent = agent.GetComponent<NavMeshAgent>();
			Animator anim = agent.GetComponent<Animator>();
			NavMeshHit sampledDestination;
			NavMesh.SamplePosition(Vector3.zero, out sampledDestination, 10f, 1);
			float distance = Vector3.Distance (agent.transform.position, sampledDestination.position);
			
			// Planning path or moving towards destination
			if (navMeshAgent.pathPending || navMeshAgent.hasPath && distance > navMeshAgent.stoppingDistance) {
				return Status.Running;
			}
			
			// Reached destination
			else if (distance <= navMeshAgent.stoppingDistance) {
				anim.SetBool ("IsWalking", false);
				return Status.Success;
			}
			
			// Can't reach destination
			else if (navMeshAgent.pathStatus == NavMeshPathStatus.PathInvalid) {
				return Status.Failure;
			}
			
			// Set destination
			else {
				navMeshAgent.SetDestination(sampledDestination.position);
				anim.SetBool ("IsWalking", true);
				return Status.Running;
			}
		}

	}
	

}